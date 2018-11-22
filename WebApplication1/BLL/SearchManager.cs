using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;
using WebApplication1.ViewModels;
using WebApplication1.Interfaces;

namespace WebApplication1.BLL
{
    public class SearchManager : ISearchManager
    {
        #region Combinations
        /*
            Комбинации:
            1 {Имя}
            2 {Имя}{Отчество}
            3 {Имя}{Фамилия}
            4 {Имя}{Отчество}{Фамилия}

            5 {Имя}{Категория}
            6 {Имя}{Отчество}{Категория}
            7 {Имя}{Отчество}{Фамилия}{Категория}

            8 {Имя}{Подкатегория}
            9 {Имя}{Отчество}{Подкатегория}
            10 {Имя}{Отчество}{Фамилия}{Подкатегория}

            11 {Название фирмы}
            12 {Название фирмы}{Категория}
            13 {Название фирмы}{Подкатегория}

            14 {Категория}
            15 {Подкатегория}
        */
        #endregion

        #region Fields
        private int NameType        { get { return (int)SearchField.Types.Name; } }
        private int SurnameType     { get { return (int)SearchField.Types.Surname; } }
        private int PatronymicType  { get { return (int)SearchField.Types.Patronymic; } }
        private int LTDType         { get { return (int)SearchField.Types.LTD; } }
        private int CategoryType    { get { return (int)SearchField.Types.Category; } }
        private int SubcategoryType { get { return (int)SearchField.Types.Subcategory; } }

        private readonly IGenericRepository<PrivateConsultant> privatesRep;
        private readonly IGenericRepository<JuridicConsultant> juridicsRep;
        private readonly IGenericRepository<Category> categoriesRep;
        private readonly IGenericRepository<Subcategory> subcategoriesRep;
        private readonly IGenericRepository<Service> servicesRep;
        #endregion

        #region ctor
        public SearchManager(IGenericRepository<PrivateConsultant> privatesRep,
                             IGenericRepository<JuridicConsultant> juridicsRep,
                             IGenericRepository<Category> categoriesRep,
                             IGenericRepository<Subcategory> subcategoriesRep,
                             IGenericRepository<Service> servicesRep)
        {
            this.privatesRep = privatesRep;
            this.juridicsRep = juridicsRep;
            this.categoriesRep = categoriesRep;
            this.subcategoriesRep = subcategoriesRep;
            this.servicesRep = servicesRep;
        }
        #endregion

        #region Private methods
        private string[] GetWords(string letters)
        {
            return letters.Split(' ');
        }

        private IList<SearchField> GetFields(string[] words)
        {
            IList<SearchField> searchFields = new List<SearchField>();
            foreach (string word in words)
            {
                // физлица
                if (IsName(word))
                {
                    searchFields.Add(new SearchField((int)SearchField.Types.Name, word));
                }
                if (IsSurname(word))
                {
                    searchFields.Add(new SearchField((int)SearchField.Types.Surname, word));
                }
                if (IsPatronymic(word))
                {
                    searchFields.Add(new SearchField((int)SearchField.Types.Patronymic, word));
                }

                // юрлица
                if (IsLTD(word))
                {
                    searchFields.Add(new SearchField((int)SearchField.Types.LTD, word));
                }

                if (IsCategory(word))
                {
                    searchFields.Add(new SearchField((int)SearchField.Types.Category, word));
                }
            }
            return searchFields;
        }

        // !!! везде GetAsync
        private bool IsName(string word)
        {
            IEnumerable<string> names = privatesRep.Get().Select(x => x.Name.ToLower());
            if (names.Any(x => x.Contains(word.ToLower())))
            {
                return true;
            }
            return false;
        }

        private bool IsPatronymic(string word)
        {
            IEnumerable<string> patronymics = privatesRep.Get().Select(x => x.Patronymic.ToLower());
            if (patronymics.Any(x => x.Contains(word.ToLower())))
            {
                return true;
            }
            return false;
        }

        private bool IsSurname(string word)
        {
            IEnumerable<string> surnames = privatesRep.Get().Select(x => x.Surname.ToLower());
            if (surnames.Any(x => x.Contains(word.ToLower())))
            {
                return true;
            }
            return false;
        }

        private bool IsCategory(string word)
        {
            IEnumerable<string> categories = categoriesRep.Get().Select(x => x.Title.ToLower());
            if (categories.Contains(word.ToLower()))
            {
                return true;
            }
            return false;
        }

        private bool IsSubcategory(string word)
        {
            IEnumerable<string> subcats = subcategoriesRep.Get().Select(x => x.Title.ToLower());
            if (subcats.Contains(word.ToLower()))
            {
                return true;
            }
            return false;
        }

        private bool IsLTD(string word)
        {
            IEnumerable<string> LTDs = juridicsRep.Get().Select(x => x.LTDTitle.ToLower());
            if (LTDs.Contains(word.ToLower()))
            {
                return true;
            }
            return false;
        }

        private SearchField GetField(IEnumerable<SearchField> searchFields, int type)
        {
            return searchFields.SingleOrDefault(x => x.Type == type);
        }

        private IList<ConsultantVM> GetSearchVMs(IEnumerable<Consultant> consultants)
        {
            if (consultants == null)
            {
                return null;
            }
            IList<ConsultantVM> vms = new List<ConsultantVM>();
            foreach (Consultant cons in consultants)
            {
                ConsultantVM vm = null;
                PrivateConsultant private_ = privatesRep.Get().SingleOrDefault(x => x.Id == cons.Id);
                if (private_ != null)
                {
                    vm = new PrivateConsultantVM
                    {
                        Id = private_.Id,
                        Name = private_.Name,
                        Surname = private_.Surname,
                        Rating = private_.Rating,
                        FeedbacksCount = new ConsultantManager().GetFeedbacksCount(private_.Id),
                        Patronymic = private_.Patronymic,
                        Services = new ServiceManager().GetVM(private_)
                    };
                }
                JuridicConsultant juridic = juridicsRep.Get().SingleOrDefault(x => x.Id == cons.Id);// !!! GetAsync
                if (juridic != null)
                {
                    vm = new JuridicConsultantVM
                    {
                        Id = juridic.Id,
                        LTDTitle = juridic.LTDTitle,
                        Rating = juridic.Rating,
                        FeedbacksCount = new ConsultantManager().GetFeedbacksCount(juridic.Id),
                        Services = new ServiceManager().GetVM(juridic)
                    };
                }
                vms.Add(vm);
            }
            return vms;
        }
        #endregion

        #region Public methods
        public IEnumerable<T> SearchPrivateConsultants<T>(IEnumerable<T> privates, string letters) where T: PrivateConsultant
        {
            string[] words = GetWords(letters);
            IList<SearchField> fields = GetFields(words);

            SearchField nameField = GetField(fields, NameType);
            SearchField surnameField = GetField(fields, SurnameType);
            SearchField patronymicField = GetField(fields, PatronymicType);
            SearchField categoryField = GetField(fields, CategoryType);
            SearchField subcategoryField = GetField(fields, SubcategoryType);

            IEnumerable<T> initialPrivates = privates;

            if (nameField != null)
            {
                privates = privates.Where(x => hasName(x, nameField));
            }
            if (patronymicField != null)
            {
                privates = privates.Where(x => hasPatromymic(x, patronymicField));
            }
            if (surnameField != null)
            {
                privates = privates.Where(x => hasSurname(x, surnameField));
            }
            if (categoryField != null)
            {
                long categoryId = categoriesRep.Get().SingleOrDefault(x => x.Title == categoryField.Value).Id;
                IEnumerable<long> privatesIdsByCat = servicesRep.Get().Where(x => x.CategoryId == categoryId)
                                                                        .Select(x => x.ConsultantId);

                privates = privates.Where(x => hasName(x, nameField) &&
                                               privatesIdsByCat.Any(id => id == x.Id));
            }
            if (initialPrivates.SequenceEqual(privates))
            {
                return new List<T>();
            }
            return privates;
        }
        
        public IEnumerable<T> SearchJuridicConsultants<T>(IEnumerable<T> juridics, string letters) where T : JuridicConsultant
        {
            string[] words = GetWords(letters);
            IList<SearchField> fields = GetFields(words);

            SearchField LTDField = GetField(fields, LTDType);
            SearchField categoryField = GetField(fields, CategoryType);

            IEnumerable<T> initialJuridics = juridics;

            if (LTDField != null)
            {
                juridics = juridics.Where(x => hasLTD(x, LTDField));
            }

            if (categoryField != null) // 5
            {
                long categoryId = categoriesRep.Get().SingleOrDefault(x => x.Title == categoryField.Value).Id;
                IEnumerable<long> juridicsIdsByCat = servicesRep.Get().Where(x => x.CategoryId == categoryId)
                                                                      .Select(x => x.ConsultantId);

                juridics = juridics.Where(x => hasLTD(x, LTDField) &&
                           juridicsIdsByCat.Any(id => id == x.Id));
            }
            if (initialJuridics.SequenceEqual(juridics))
            {
                return new List<T>();
            }
            return juridics;
        }
        #endregion

        #region Delegates
        Func<PrivateConsultant, SearchField, bool> hasName = delegate (PrivateConsultant private_, SearchField nameSF)
        {
            return private_.Name.ToLower().Contains(nameSF.Value.ToLower());
        };

        Func<PrivateConsultant, SearchField, bool> hasSurname = delegate (PrivateConsultant private_, SearchField surnameSF)
        {
            return private_.Surname.ToLower().Contains(surnameSF.Value.ToLower());
        };

        Func<PrivateConsultant, SearchField, bool> hasPatromymic = delegate (PrivateConsultant private_, SearchField patromymicField)
        {
            return private_.Patronymic.ToLower().Contains(patromymicField.Value.ToLower());
        };

        Func<JuridicConsultant, SearchField, bool> hasLTD = delegate (JuridicConsultant juridic, SearchField ltd)
        {
            return juridic.LTDTitle.ToLower().Contains(ltd.Value.ToLower());
        };
        #endregion
    }
}