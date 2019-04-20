namespace cinefilo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using cinefilo.Helpers;
    using cinefilo.Interfaces;
    using cinefilo.Models;
    using cinefilo.Models.ws;
    using cinefilo.Services;
    using cinefilo.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Util;
    using Xamarin.Forms;

    public class TriviaViewModel : BaseViewModel
    {
        private DialogLoading dialogLoading;

        public TriviaViewModel()
        {
            dialogLoading = new DialogLoading();
            executeAction(false);
            OnLoad();
        }

        private int countObtenerPregunta = 0;
        private long? codigoTrivia { get; set; }
        private List<TriviaPregunta> preguntas { get; set; }
        private GetListTrivia getListTrivia { get; set; }
        private TriviaPregunta triviaPregunta;
        public TriviaPregunta TriviaPregunta { get { return triviaPregunta; } set { SetValue(ref triviaPregunta, value); } }

        private ObservableCollection<TriviaRespuesta> respuestas;
        public ObservableCollection<TriviaRespuesta> Respuestas { get { return respuestas; } set { SetValue(ref respuestas, value); } }
        private bool isEnabled;
        public bool IsEnabled { get { return isEnabled; } set { SetValue(ref isEnabled, value); } }
        private ImageSource qr;
        public ImageSource QR { get { return qr; } set { SetValue(ref qr, value); } }
        private bool respuestaProcesada = false;

        public async void OnLoad()
        {
            try
            {
                await ShowDialog();

                preguntas = new List<TriviaPregunta>();
                Respuestas = new ObservableCollection<TriviaRespuesta>();
                TriviaPregunta = new TriviaPregunta();
                countObtenerPregunta = 0;
                respuestaProcesada = false;

                GetListTrivia getListTrivia = await ApiService.WS_GetListTrivia<GetListTrivia>(MainViewModel.GetInstace().Session.Token);
                if (getListTrivia != null)
                {
                    if (getListTrivia.ErrorCode == 0 && getListTrivia.preguntas != null)
                    {
                        codigoTrivia = getListTrivia.codigoTrivia;
                        preguntas = new List<TriviaPregunta>(getListTrivia.preguntas);

                        var getRegisterIntentTrivia = await ApiService.WS_GetTriviaUser<RegisterIntentTrivia>
                                (MainViewModel.GetInstace().Session.Token, codigoTrivia.Value);
                        if (getRegisterIntentTrivia != null)
                        {
                            if (getRegisterIntentTrivia.ErrorCode == 0 && getRegisterIntentTrivia.codigoTriviaUsuario != null)
                            {
                                respuestaProcesada = true;

                                if (getRegisterIntentTrivia.estadoRespuesta && getRegisterIntentTrivia.estadoCobro == false)
                                {
                                    generarQR(getRegisterIntentTrivia.codigoTriviaUsuario.Value.ToString());
                                }
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }

                actualizarPregunta();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Util.ShowMessage(Languages.Alert, Languages.System_Process_Error, Languages.Accept, null);
            }
            finally
            {
                await CloseDialog();
            }
        }

        private bool actualizarPregunta()
        {
            bool nuevaPregunta = false;

            if (preguntas.Count > countObtenerPregunta)
            {
                nuevaPregunta = true;
                //OBTENEMOS LA PRIMERA PREGUNTA DE LA LISTA
                TriviaPregunta = preguntas[countObtenerPregunta];
                //ACTUALIZAMOS EL LISTADO DE RESPUESTAS
                Respuestas = new ObservableCollection<TriviaRespuesta>(TriviaPregunta.respuestas);
                //INCREMENTAMOS EL CONTADOR DE LA PREGUNTA PARA IR SACANDO LAS NUEVAS PREGUNTAS
                countObtenerPregunta++;
            }
            return nuevaPregunta;
        }

        private void generarQR(string contentQR)
        {
            byte[] temp = DependencyService.Get<IShare>().ConvertImageStream(contentQR);
            Stream stream_code = new MemoryStream(temp);
            QR = ImageSource.FromStream(() => { return stream_code; });
            Util.ShowNavigationPageDefault(new TriviaQR());
        }

        public ICommand ValidarRespuestaCommand
        {
            get
            {
                return new RelayCommand(ValidarRespuesta);
            }
        }

        private async void ValidarRespuesta()
        {
            try
            {
                if (Respuestas.Count > 0)
                {
                    int countAnswerSelected = 0;
                    bool estadoRespuesta = false;

                    foreach (TriviaRespuesta item in Respuestas)
                    {
                        if (item.estadoSeleccion)
                        {
                            countAnswerSelected++;

                            if (item.estadoRespuesta == item.estadoSeleccion)
                            {
                                estadoRespuesta = true;
                                break;
                            }
                        }
                    }

                    if (countAnswerSelected == 0)
                    {
                        await Util.ShowMessage(Languages.Alert, "Porfavor seleccione una respuesta", Languages.Accept, null);
                        return;
                    }

                    if (respuestaProcesada)
                    {
                        await Util.ShowMessage(Languages.Alert, "Ya no tiene mas intentos de trivia, intentelo en otra oportunidad", Languages.Accept, null);
                        return;
                    }

                    if (estadoRespuesta)
                    {
                        if (actualizarPregunta())
                        {
                            await Util.ShowMessage(Languages.Alert, "Respuesta correcta, conteste la siguiente pregunta", Languages.Accept, null);
                        }
                        else
                        {
                            var getRegisterIntentTrivia = await ApiService.WS_RegisterIntentTrivia<RegisterIntentTrivia>
                                (MainViewModel.GetInstace().Session.Token, codigoTrivia.Value, estadoRespuesta);
                            if (getRegisterIntentTrivia != null)
                            {
                                if (getRegisterIntentTrivia.ErrorCode == 0)
                                {
                                    respuestaProcesada = true;
                                    generarQR(getRegisterIntentTrivia.codigoTriviaUsuario.Value.ToString());
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        var getRegisterIntentTrivia = await ApiService.WS_RegisterIntentTrivia<RegisterIntentTrivia>
                                (MainViewModel.GetInstace().Session.Token, codigoTrivia.Value, estadoRespuesta);
                        if (getRegisterIntentTrivia != null)
                        {
                            if (getRegisterIntentTrivia.ErrorCode == 0)
                            {
                                respuestaProcesada = true;
                                await Util.ShowMessage(Languages.Alert, "Respuesta incorrecta, intentelo en otra oportunidad", Languages.Accept, null);
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    await Util.ShowMessage(Languages.Alert, "No se encontraron respuestas disponibles para seleccionar", Languages.Accept, null);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Util.ShowMessage(Languages.Alert, Languages.System_Process_Error, Languages.Accept, null);
            }
        }

        public ICommand SelectedRadioButtonTriviaCommand => new Command<TriviaRespuesta>((TriviaRespuesta value) =>
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (IsEnabled)
                    {
                        executeAction(true);

                        List<TriviaRespuesta> RespuestasCopy = new List<TriviaRespuesta>(Respuestas);
                        Respuestas = new ObservableCollection<TriviaRespuesta>();

                        foreach (TriviaRespuesta item in RespuestasCopy)
                        {
                            if (item.codigoTriviaRespuesta == value.codigoTriviaRespuesta)
                            {
                                item.estadoSeleccion = true;
                            }
                            else
                            {
                                item.estadoSeleccion = false;
                            }
                            Respuestas.Add(item);
                        }

                        executeAction(false);
                    }
                }
                catch (Exception ex)
                {
                    await Util.ShowMessage(Languages.Alert, Languages.System_Process_Error, Languages.Accept, null);
                    executeAction(false);
                }
            });
        });

        private async Task ShowDialog()
        {
            try { executeAction(true);  await PopupNavigation.Instance.PushAsync(dialogLoading, true); } catch (Exception) { }
        }

        private async Task CloseDialog()
        {
            try { executeAction(false); await PopupNavigation.Instance.RemovePageAsync(dialogLoading, true); } catch (Exception) { }
        }

        private void executeAction(bool active)
        {
            if (active)
            {
                IsEnabled = false;
            }
            else
            {
                IsEnabled = true;
            }
        }
    }
}
