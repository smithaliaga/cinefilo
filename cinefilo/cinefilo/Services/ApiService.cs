namespace cinefilo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Helpers;
    using Views;
    using Xamarin.Forms;
    using Util;
    using System.Diagnostics;
    using cinefilo.Models.ws;

    public class ApiService
    {
        #region CHECK_CONNECTION
        public static async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.ErrorInternetConnection,
                };
            }
            
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.ErrorInternetConnection,
                };
            }
            
            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }
        #endregion

        #region METHODS
        public static async Task<UserAuthenticate> WS_UserAuthenticate<T>(
            string username,
            string password,
            string language,
            string deviceInfo)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "usuario", username },
                    { "clave", password },
                    { "idioma", language },
                    { "infoDispositivo", deviceInfo },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequest<Response>(Globales.WS_Url + Globales.WS_UserAuthenticate, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<UserAuthenticate>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<ProcessQR> WS_ProcessQR<T>(
            string token, string idDiscount)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "Token", token },
                    { "IdDiscount", idDiscount },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequest<Response>(Globales.WS_Url + Globales.WS_ProcessQR, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<ProcessQR>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<GetListMovie> WS_GetListMovie<T>(
            string token)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "token", token },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequest<Response>(Globales.WS_Url + Globales.WS_GetListMovie, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<GetListMovie>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<GetListTrivia> WS_GetListTrivia<T>(
            string token)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "token", token },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequest<Response>(Globales.WS_Url + Globales.WS_GetListTrivia, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<GetListTrivia>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<EntityWSBase> WS_SendTransactionBuyTicket<T>(
            string token, long? codigoPelicula, string numeroTarjeta, string nombreTitular, string fechaExpiracion, string codigoSeguridad)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "token", token },
                    { "codigoPelicula", codigoPelicula },
                    { "numeroTarjeta", numeroTarjeta },
                    { "nombreTitular", nombreTitular },
                    { "fechaExpiracion", fechaExpiracion },
                    { "codigoSeguridad", codigoSeguridad },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequest<Response>(Globales.WS_Url + Globales.WS_SendTransactionBuyTicket, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<EntityWSBase>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<EntityWSBase> WS_SendException<T>(
            string exception,
            string sessionId)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "exception", exception },
                    { "sessionId", sessionId },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequestException<Response>(Globales.WS_Url + Globales.WS_SendException, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<EntityWSBase>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<RegisterIntentTrivia> WS_RegisterIntentTrivia<T>(
            string token, long codigoTrivia, bool estadoRespuesta)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "token", token },
                    { "codigoTrivia", codigoTrivia },
                    { "estadoRespuesta", estadoRespuesta },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequest<Response>(Globales.WS_Url + Globales.WS_RegisterIntentTrivia, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<RegisterIntentTrivia>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        public static async Task<RegisterIntentTrivia> WS_GetTriviaUser<T>(
            string token, long codigoTrivia)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "token", token },
                    { "codigoTrivia", codigoTrivia },
                };

                string jsonData = JsonConvert.SerializeObject(parameters, Formatting.Indented);
                var response = await SendRequest<Response>(Globales.WS_Url + Globales.WS_GetTriviaUser, jsonData);

                if (response == null)
                {
                    return null;
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<RegisterIntentTrivia>(response.Result);
                    return model;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }

        #endregion

        #region DEFAULT_REQUEST
        public static async Task<Response> SendRequest<T>(string url, string parameters)
        {
            try
            {
                var connection = await CheckConnection();
                if (!connection.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(Languages.Alert, connection.Message, Languages.Accept);
                    return null;
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (cert.Subject.Contains(Globales.HOSTNAME_TRUST))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };

                var content = new StringContent(
                    parameters,
                    Encoding.UTF8, "application/json");

                System.Console.WriteLine("content:" + content);

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.PostAsync("", content);

                if (!response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage.DisplayAlert(Languages.Alert, Languages.ErrorNoConnection, Languages.Accept);
                    return null;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    System.Console.WriteLine("result:" + result);
                    var model = JsonConvert.DeserializeObject<EntityWSBase>(result);

                    switch (model.ErrorCode)
                    {
                        case 0:
                            {
                                return new Response
                                {
                                    IsSuccess = true,
                                    Message = "Ok",
                                    Result = result,
                                };
                            }
                        case 100:
                            {
                                await Util.ShowMessage(Languages.Alert, model.ErrorMessage, Languages.Accept, async () =>
                                {
                                    Application.Current.MainPage = new LoginPage();
                                });
                                return null;
                            }
                        default:
                            {
                                await Util.ShowMessage(Languages.Alert, model.ErrorMessage, Languages.Accept, null);
                                return null;
                            }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                await Application.Current.MainPage.DisplayAlert(Languages.Alert, ex.Message, Languages.Accept);
                return null;
            }
        }

        public static async Task<Response> SendRequestException<T>(string url, string parameters)
        {
            try
            {
                var connection = await CheckConnection();
                if (!connection.IsSuccess)
                {
                    Debug.WriteLine(connection.Message);
                    return null;
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) =>
                {
                    if (cert.Subject.Contains(Globales.HOSTNAME_TRUST))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                };

                var content = new StringContent(
                    parameters,
                    Encoding.UTF8, "application/json");

                var client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.PostAsync("", content);
                Console.WriteLine(response);
                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(Languages.ErrorNoConnection);
                    return null;
                }
                else
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<EntityWSBase>(result);

                    switch (model.ErrorCode)
                    {
                        case 0:
                            {
                                return new Response
                                {
                                    IsSuccess = true,
                                    Message = "Ok",
                                    Result = result,
                                };
                            }
                        default:
                            {
                                Debug.WriteLine(model.ErrorMessage);
                                return null;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
        #endregion

        public async Task<Response> Get<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    id);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request, Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();

                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Put<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(
                    request,
                    Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.PutAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var newRecord = JsonConvert.DeserializeObject<T>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = result,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Delete<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            T model)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(tokenType, accessToken);
                var url = string.Format(
                    "{0}{1}/{2}",
                    servicePrefix,
                    controller,
                    model.GetHashCode());
                var response = await client.DeleteAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                return new Response
                {
                    IsSuccess = true,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
