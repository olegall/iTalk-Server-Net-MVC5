
Кавычки одинарные для упрощения восприятия


host: u0542276.plsk.regruhosting.ru или localhost:54788

------------- Категории ------------------------------
Запрос GET - получить категории с подкатегориями:
host/api/categories

Ответ:
[
	{
		'Id': 1,
		'Title': 'Категория 1',
		'Image' 'path/xxx.jpg': 
		'Subcategories':	[
								{'Id': 1, 'Title': 'Подкатерия 1'},
								{'Id': 2, 'Title': 'Подкатерия 2'},
								{'Id': 3, 'Title': 'Подкатерия 3'},
							]
							
	},
	{
		'Id': 75, (для перехода к отзывам этого консультанта)
		'Title': 'Категория 2',
		'Image' 'path/xxx.jpg': 
		'Subcategories':	[
								{'Id': 4, 'Title': 'Подкатерия 4'},
								{'Id': 5, 'Title': 'Подкатерия 5'},
								{'Id': 6, 'Title': 'Подкатерия 6'},
							]
							
	}
]


------------ Стр 8. Регистрация / вход клиента -------------------

Запрос POST - авторизоваться через Firebase
host/api/auth/AuthFirebase

Запрос POST 
host/api/auth/vk

Запрос POST 
host/api/auth/fb

Запрос POST 
host/api/auth/ok

Запрос POST 
host/api/auth/gp

Для всех параметр в form-data
text keys:
token

Для всех ответ: true


------------ Стр 10. Поиск консультантов -------------------
Запрос POST - найти консультанта по ключевому слову:
host/api/consultants/search

text keys:
letters - вводимые символы

Ответ (поля, для которых значения null, false или 0 - не учитывать):
[
    {
        "Name": "Сергей",
        "Surname": "Лавров",
        "Patronymic": "Петрович",
        "Photo": null,
        "GalleryImages": null,
        "Id": 5,
        "Rating": 11,
        "FeedbacksCount": 2,
        "Services": [
            {
                "Title": "Наркомания",
                "Description": "Помогу.",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 1000,
                "Duration": 100
            },
            {
                "Title": "Алкоголизм",
                "Description": "Избавляю от алкогольной зависимости. Анонимно",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 1500,
                "Duration": 150
            }
        ],
        "AccountNumber": null,
        "Free": false,
        "FreeDate": 0,
        "LegalEntity": false,
        "Favorite": false
    },
    {
        "Name": "Сергей",
        "Surname": "Иванов",
        "Patronymic": "Петрович",
        "Logo": null,
        "GalleryImages": null,
        "Id": 11,
        "Rating": 0,
        "FeedbacksCount": 0,
        "Services": [],
        "AccountNumber": null,
        "Free": false,
        "FreeDate": 0,
        "LegalEntity": false,
        "Favorite": false
    }
]

------------ Стр 11. Список консультантов -------------------
Запрос GET - получить консультантов подкатегории:
host/api/consultants/subcategory/{long id}

Ответ:
[
    {
        "Name": "Сергей",
        "Surname": "Лавров",
        "Patronymic": "Петрович",
        "Photo": "path/profileimage1.jpg",
        "GalleryImages": [
            "path/galleryImage1.jpg",
            "path/galleryImage2.jpg",
        ],
        "Id": 5,
        "Rating": 11,
        "FeedbacksCount": 2,
        "Services": [
            {
                "Title": "Наркомания",
                "Description": "Помогу.",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 1000,
                "Duration": 100
            },
            {
                "Title": "Алкоголизм",
                "Description": "Избавляю от алкогольной зависимости. Анонимно",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 1500,
                "Duration": 150
            }
        ],
        "AccountNumber": 123456789,
        "Free": false,
        "FreeDate": 1534513658,
        "LegalEntity": false,
        "Favorite": true
    },
    {
        "Name": "Прошляков",
        "Surname": "Александр",
        "Patronymic": "Алексеевич",
        "Photo": "path/img2.png",
        "GalleryImages": ["path/img2.png"],
        "Id": 9,
        "Rating": 100,
        "FeedbacksCount": 0,
        "Services": [
            {
                "Title": "Услуга 3",
                "Description": "Описание услуги 3",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 2000,
                "Duration": 3
            }
        ],
        "AccountNumber": 123456789,
        "Free": false,
        "FreeDate": 1534513658,
        "LegalEntity": false,
        "Favorite": false
    },
    {
        "LTDTitle": "LTDTitle",
        "OGRN": "1031628201098",
        "INN": "430601071197",
        "SiteUrl": "site.com",
        "Logo": "logo.png",
        "OGRNCertificate": "gggg",
        "BankAccountDetails": "12345",
        "Id": 0,
        "Rating": 50,
        "FeedbacksCount": 1,
        "Services": [
            {
                "Title": "Услуга 4",
                "Description": "Описание...",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 1200,
                "Duration": 5
            },
            {
                "Title": "Услуга такая-то",
                "Description": "descr",
                "Category": "Врачи",
                "Subcategory": "Дерматологи",
                "Cost": 1000,
                "Duration": 100
            },
            {
                "Title": "название",
                "Description": "описание",
                "Category": "Врачи",
                "Subcategory": 'Терапевты',
                "Cost": 1000,
                "Duration": 100
            },
            {
                "Title": "название",
                "Description": "описание",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 1000,
                "Duration": 100
            },
            {
                "Title": "название",
                "Description": "описание",
                "Category": "Врачи",
                "Subcategory": "Психотерапевты",
                "Cost": 2000,
                "Duration": 201
            }
        ],
        "AccountNumber": 12345,
        "Free": true,
        "FreeDate": 1536472800,
        "LegalEntity": true,
        "Favorite": false
    }
]
------------ Стр. 12 Подробная карточка консультанта -------------------

Запрос GET - получить консультанта для карточки:
host/api/consultants/{long id}

Ответ:

{
	'Id': 75, (для перехода к отзывам этого консультанта)
	или
	'Phone': '9537448298'
	
	Если физлицо
	'Name': 'Лавров',
	'Surname': 'Сергей',
	'Patronymic': 'Алексеевич',
	'Photo': 'path/image51.jpg',
	
	Если юрлицо
	'OGRN': 'ОГРН такой-то',
	'Logo': 'path/image51.jpg',
	
	Для всех
	'GalleryImages': [
						'path/image51.jpg',
						'path/image78.jpg',
						'path/image777.png'
					 ],
	'Rating': 100.25,
	'FeedbacksCount': 2,
	'Services':	[
					{
						'Title': 'Услуга 11 1', /*(первые 2 цифры - кат. и подкат., последняя - номер услуги)*/
						'Description': 'Описание...'
						'Category': 'Категория 1',
						'Subcategory': 'Подкатегория 1 кат-и 1',
						'Cost': 1500,
						'Duration': '2 дня',
					},
					{
						'Title': 'Услуга 12 2',
						'Description': 'Описание...'						
						'Category': 'Категория 1',
						'Subcategory': 'Подкатегория 2 кат-и 1',
						'Cost': 700, 
						'Duration': '7 часов'
					}
				],
	'LegalEntity': true/false
}


!!! Длительность?

------------ Стр. 13, 14 Оформление заказа -------------------

Запрос GET - получить список услуг для офомления заказа
host/api/services/

Ответ:
[
	{'Id': 1, 'Title': 'Репетитор по математике' }
	{'Id': 2, 'Title': 'Репетитор по физике' }
]

-------------------------------------------------------------------------

Запрос GET - получить типы консультаций для оформления заказа
host/api/getConsultationTypes

Ответ:
[
	{'Id': 1, 'Type': 'Аудио' }
	{'Id': 2, 'Type': 'Видео' }
]

-------------------------------------------------------------------------

Запрос POST - оформить заказ:
host/api/orders

Параметры body - form-data
-------------------------------------
text keys:
long ServiceId 
long date Date 
boolean VideoCall 
String Comment 



Ответ: true

------------ Стр. 16 ----------------------------------------

Запрос PUT - Подтвердить заказ консультантом
host/api/orders/{id}/confirm

Ответ: true

--------------------------------------------------------------------------

Запрос PUT - Отменить заказ клиентом
host/api/orders/{id}/CancelByClient

Ответ: true

--------------------------------------------------------------------------

Запрос PUT - Отменить заказ консультантом
host/api/orders/{id}/CancelByCons

Ответ: true

------------ Стр. 17 Избранное ------------------------------

Запрос POST - добавить в избранное
host/api/favorites/{clientId}/{consultantId}

Ответ: true

--------------------------------------------------------------

Запрос GET - получить избранных консультантов клиента:
host/api/favorites/client/{long id}

Ответ:
[
	{
		'Id': 1, (для перехода к отзывам этого консультанта)
		'Name': 'Лавров Сергей Алексеевич',
		'Rating': 100.25,
		'FeedbacksCount': 2,
		'Services':	см. в карточке консультанта
	},
	{
		'Id': 2,
		'Name': 'Окна профи',
		'Rating': 100.25,
		'FeedbacksCount': 2,
		'Services':	см. в карточке консультанта
	},
]

--------------------------------------------------------------

Запрос DELETE - удалить из избранного
host/api/favorites/{long clientId}/{long consultantId}

Ответ: true

------------ Стр 18 Мои заказы ------------------------------

Запрос GET - получить заказы клиента:
host/api/orders/client/{long id}

Ответ:
[
	{
		'Id': 1, (для обработки нажатия кнопки)
		'Number': 1145554,
		'Date': 123456789, 
		'ConsultantName': 'Сергеев С.В.'
		'ServiceName': 'Помощь такая-то'
	},
	{
		'Id': 2,	
		'Number': 1145555,
		'Date': 123456789,
		'ConsultantName': 'Лавров С.Е.'
		'ServiceName': 'Помощь такая-то'
	}
]

------------ Стр 19 Личный кабинет -------------------

Запрос GET - получить инфо о клиенте:
host/api/clients/{long id}

Ответ:
{
	'Id': 1,
	'Name': 'Сергеев',
	'Phone': '9537448298'
}

------------ Стр 20 Настройки -------------------
Запрос GET - получить платёжную информацию клиента:
host/api/paymentdata/client/{long id}

Ответ: (Id - для кнопок редактирования и удаления)

[
	{'Id': 1, 'Number': '1111-1111-2222-3333'},
	{'Id': 2, 'Number': '1112-1111-2222-3333'},
	{'Id': 3, 'Number': '1113-1111-2222-3333'},
]

-------------------------------------------------------------

Запрос PUT - изменить номер карты клиента:
host/api/paymentdata/client/{long id}

Ответ: true

-------------------------------------------------------------

Запрос DELETE - удалить номер карты клиента:
host/api/paymentdata/client/{long id}

Ответ:
пока непонятно

-------------------- стр 26 РЕГИСТРАЦИЯ КОНСУЛЬТАНТА (ФИЗ. ЛИЦО) ------------------ 
Зарегистрировать физлицо
Запрос POST
host/api/consultants/RegisterPrivate

															  
Параметры body  (лучше сохранить порядок)
-------------------------------------
text keys:
name - имя 				
surname - фамилия 			
patronymic - отчество 			
phone - телефон 			
email - емайл


file keys:
photo Фото
passportscan скан паспорта
doc1...doc5 Документы об образовании (до 5)
-------------------------------------
	
Ответ: 
{
	'Id': 1,
	'LegalEntity': true/false
}

---------------------------------------------------------------------------

Зарегистрировать сразу много услуг
Запрос POST
host/api/services



Параметры body - массив услуг
-------------------------------------

Ответ: true

-------------------- стр 27 РЕГИСТРАЦИЯ КОНСУЛЬТАНТА (ЮР. ЛИЦО) ------------------ 

Запрос POST
host/api/consultants/RegisterJuridic

Параметры body - form-data (лучше сохранить порядок)
-------------------------------------
text keys:
ltdtitle - название фирмы: 	
ogrn - ОГРН 			
inn - ИНН
phone - телефон
email - емайл
site - сайт
account - номер счёта

file keys:
logo - лого 				
doc1...doc5 - сертификаты, свидетельства, лицензии (до 5)

Ответ:
{
	'Id': 1,
	'LegalEntity': true/false
}

----------------------------------------------------------------------------------


Зарегистрировать сразу много услуг (см. у физлица)


------------ стр 28 ЗАПОЛНЕНИЕ КАРТОЧКИ УСЛУГИ ----------------------

Запрос POST - добавить услугу
host/api/services

Параметры body - form-data
-------------------------------------
text keys:
consid - консультант
category - категория
subcategory - подкатегория
title - название
description - описание
cost - цена
duration - длительность
available - доступен ли
availableperiod - период доступности


file keys:
image - изображение

Ответ: true

-------------------------------------

Запрос POST - добавить много услуг
host/api/services/many

Комбинации категорий/подкатегорий

---------------------------------------
Категория 			Подкатегория
---------------------------------------
Существующая		Существующая
Существующая		Предлагаемая(новая)
Предлагаемая(новая)	Предлагаемая(новая)

Существующая - число
Предлагаемая - null

text keys:
services - строка:
{
	'ConsId': 1,
	'Params':	[
					{
						'Category': '1',
						'Subcategory': '1',
						'Title': 'title',
						'Description': 'descr',
						'Cost': 1000,
						'Duration': 60,
						'Available': true
						'AvailablePeriod': 300
					},
					{
						'Category': '2',
						'Subcategory': 'Подкатегория 2',
						'Title': 'title',						
						'Description': 'descr',
						'Cost': 2000,
						'Duration': 120,
						'Available': true,
						'AvailablePeriod': 300
						
					},
					{
						'Category': 'Категория 3',
						'Subcategory': 'Подкатегория 3',
						'Title': 'title',						
						'Description': 'descr',
						'Cost': 2000,
						'Duration': 120,
						'Available': true,
						'AvailablePeriod': 300
					}					
				]
}

Category, Subcategory - это строка - Id или Название новой

file keys
image1...imageN - изображения


------------------------------------------------------------------

Запрос PUT - редактировать услугу
host/api/services

Параметры body: см. стр.28 - Добавить услугу
+ id - редактируемая услуга


Ответ: true

------------------------------------------------------------------

Запрос GET - получить категории (для клиента - просто категории, для консультанта - с подкатегориями)
host/api/categories/{int offset}/{int limit}

Ответ: 
См. Список категорий, не учитывайте Subcategories

------------------------------------------------------------------

Запрос POST - добавить изображение к категории
host/api/categories/{long id}/image

Ответ: true

Запрос GET - 
получить подкатегории по категории
host/api/subcategories/{long categoryId}/{int offset}/{int limit}

------------ Стр 30 Главный экран консультанта -------------------
Запрос GET - получить инфо консультанта:
host/api/consultants/{long id}

{
	'Id': 1,
	 ФИО/ОГРН (см. в карточке консультанта)
	'Phone': '9537448298',
	'Services':	см. в карточке консультанта,
}

------------------------------------------------------------------

Запрос DELETE - "удалить" (скрыть) услугу
host/api/services/{long id}

Ответ: true

------------------------------------------------------------------

Запрос PUT - редактировать поля физлица
localhost:54778/api/consultants/private/{long id}

Параметры body: см. параметры при регистрации

Ответ: true

----------------------------------------------------------------

Запрос PUT - редактировать поля юрлица
localhost:54778/api/consultants/juridic/{long id}

Параметры body: см. параметры при регистрации

Ответ: true

----------------------------------------------------------------

Запрос POST - добавить изображение консультанта - при его редактировании
host/api/consultants/{long id}/image

Ответ: true

----------------------------------------------------------------

Запрос DELETE - Удалить изображение консультанта - при его редактировании
host/api/consultants/image/{long id}

Ответ: true



------------ Стр 31 Уведомления о запросах ---------------------
Запрос GET - получить уведомления для консультанта
host/api/notifications/{long id} - id - консультанта

Ответ:
[
    {
        'Description': 'Добрый день, мне нужна консультация',
        'DateTime': 123456789,
		'ClientName': 'Oleg',
		'ServiceTitle': 'Врачи'
    },
    {
        'Description': 'Проконсультируйте!',
        'DateTime': '17.08.2018 17:40',
		'ClientName': 'Oleg',
		'ServiceTitle': 'Врачи'		
    }
]



------------ Стр 32 Мои заказы ---------------------------------
Запрос GET - получить заказы консультанта:
host/api/orders/consultant/{long id}

Ответ:
[
	{
		'Id': 1,
		'Service': 'Услуга 1', 
		'Status': 'Выполнено',
		'PaymentStatus': 'Оплачено'
		'Date': '7 июня 2018 14:00',

	},
	{
		'Id': 2,
		'Service': 'Услуга 2', 
		'Status': 'Выполнено',
		'PaymentStatus': 'Оплачено'
		'Date': '7 июня 2018 14:00',
	}
]

----------------------------------------------------------------

Запрос GET - получить инфо о загруженности консультанта:
host/api/consultants/{long id}/loading

Ответ:
[
	{'Level': 70, 'Date': 'Сегодня'}
	{'Level': 'Доступен', 'Date': 'Завтра с 12:00'}
]

----------- Авторизация через FireBase ------------------------
Запрос POST 
host/api/login/AuthFirebase

Параметры body - form-data
-------------------------------------
file keys:
token - токен

Ответ: true

---------------- Для панели администратора ---------------------

Запрос POST - добавить категорию
host/api/categories

Параметры body - form-data
-------------------------------------
text keys:
title - название
description - описание

Ответ: true

----------------------------------------------------------------

Запрос DELETE - скрыть категорию
host/api/categories/{long id}

Ответ: true

----------------------------------------------------------------

Запрос POST - добавить подкатегорию
host/api/subcategories

Параметры body - form-data
-------------------------------------
text keys:
title - название

Ответ: true

----------------------------------------------------------------

Запрос DELETE - скрыть подкатегорию
host/api/subcategories/{long id}

Ответ: true

----------------------------------------------------------------



----------- Доработки -------------------------------

Запрос GET - получить инфо о заказе:
host/api/orders/{long id}

Ответ:
[
	{
		'Id': 1,
		'Number': 123456,
        'Client': 'Иванов Сергей Петрович',
        'Consultant': 'Петров Пётр Петрович',
        'Service': 'Услуга 1',
        'Status': 'Принят консультантом',
        'PaymentStatus': 'Оплачено',
        'ConsultationTypeId': 'Видео',
        'DateTime': 04.09.2017 16:00
        'Comment': 'Комментарий',
        'ConfirmedByClient': true,
        'ConfirmedByConsultant': false,
		'Sum': 1500
		'BankAccountDetails': напишу позже
		'ITalkCommittee': 500.7
	}
]

Запрос DELETE - удалить клиента:
host/api/clients/{long id}

----------------------------------------------------------------

Запрос UPDATE - редактировать имя клиента
host/api/clients

text keys:
name - имя
AdPush - подписаться/отписаться от уведомлений

----------------------------------------------------------------

Запрос POST
host/api/service/pay - оплатить услугу? заказ?  !!! непонятно
Параметр "OrderId" long

----------------------------------------------------------------

Запрос GET - получить заказы для клиента
host/api/orders/{int offset}/{int limit}

Ответ:
[
	{
		Status: 'Принят консультантом',
		StatusCode: 1,
		ServiceId: 1,
		Image: 'path/xxx.jpg'
	},
	{
		Status: 'Начат клиентом',
		StatusCode: 2,
		ServiceId: 2,
		Image: 'path/xxx.png'
	},
]

----------------------------------------------------------------

Запрос GET - Получить инфо о клиенте для карточки

host/api/clients/{bool adPush}

----------------------------------------------------------------

Запрос Update - редактировать инфо пользователя
host/api/clients/{name}/{adPush}
----------------------------------------------------------------

Запрос GET - Получить консультантов
api/consultants/{int offset}/{int limit}/{long subcategoryId}/{bool free}/{bool onlyFavorite}/{string filter}

----------------------------------------------------------------
Запрос GET - Получить время консультантов по дню
api/consultants/availableTimes/{long time}

Ответ:
[12345589, 1234888789, 123456789]



----- ЧАТ -------

Запустить чат (буду делать скорее всего я)
GET: api/chat/start
Ответ: true

вебсокет запрос - подключиться к чату
{connecteeId: 1, orderId: 1} - как строка

вебсокет запрос - отправить сообщение в чат
параметры:
string text

вебсокет ответ:
{Id: 19,Text: yyu,Image: xxx.jpg,SenderName: Александр,SenderId: 9,Date: 1536241898}

Запрос GET - Получить историю сообщений
api/chat/history/{long orderId}/{long lastMessageId}




------------- ПУТИ ДО ИЗОБРАЖЕНИЙ (path) ------------------
консультанты: host/consimage/
галерея: host/galleryimage/





