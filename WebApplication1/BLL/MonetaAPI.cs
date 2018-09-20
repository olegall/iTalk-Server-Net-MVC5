using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace DiscountSystem.Utils.MonetaApi
{
    public class MonetaApi
    {
        private const string Username = "secretary@multiplan.me";
        private const string Password = "12345678";

        private const string Payer = "79992502";
        private const string PaymentPassword = "123456";

        private const string hostUrl = "https://demo.moneta.ru/services";
        private readonly RestClient client;

        public MonetaApi()
        {
            client = new RestClient(hostUrl);
        }

        public string Payment(decimal ammount, string guid, string description, string cardNumber, string cardExpiration)
        {
            if (string.IsNullOrWhiteSpace(cardExpiration) || string.IsNullOrWhiteSpace(cardNumber))
                return null;

            var request = new RestRequest(Method.POST);

            // TODO: test dynamic obj
            var requestContent = new
            {
                Envelope = new
                {
                    Header = new
                    {
                        Security = new
                        {
                            UsernameToken = new
                            {
                                Username = Username,
                                Password = Password
                            }
                        }
                    },
                    Body = new
                    {
                        PaymentRequest = new
                        {
                            payer = Payer,
                            payee = "279",
                            amount = ammount,
                            isPayerAmount = true,
                            paymentPassword = PaymentPassword,
                            clientTransaction = guid,
                            description = description,
                            operationInfo = new
                            {
                                attribute = new[] {
                                    new {
                                        key= "PAYEECARDNUMBER",
                                        value= cardNumber
                                    },
                                    new {
                                        key= "CARDEXPIRATION",
                                        value= cardExpiration
                                    }
                                }
                            }
                        }
                    }
                }
            };

            request.AddJsonBody(requestContent);
            var res = client.Execute(request);

            dynamic content = JsonConvert.DeserializeObject(res.Content);
            var attributes = (Newtonsoft.Json.Linq.JArray)content?.Envelope?.Body?.PaymentResponse?.attribute;
            if (attributes == null)
                return null;

            var list = attributes.ToObject<List<Attribute>>();
            if (list == null)
                return null;

            return list.Where(x => x.Key == "statusid").Select(x => x.Value).FirstOrDefault();
        }

        private class Attribute
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}