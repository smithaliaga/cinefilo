using Plugin.MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static cinefilo.Models.ws.GetListMovie;

namespace cinefilo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailMoviePage : ContentPage
	{
        Movie movie;

        public DetailMoviePage (Movie movie)
		{
			InitializeComponent ();
            this.movie = movie;
            Title = movie.Name;
		}

        private bool isEnabled = true;
        private bool isRunningVideo = false;

        private void Video_Clicked(object sender, EventArgs e)
        {
            RunVideo();
        }

        private void RunVideo()
        {
            if (isEnabled)
            {
                isEnabled = false;

                if (isRunningVideo)
                {
                    CrossMediaManager.Current.Stop();
                    isRunningVideo = false;
                }
                else
                {
                    CrossMediaManager.Current.Play(movie.URL, Plugin.MediaManager.Abstractions.Enums.MediaFileType.Video);
                    isRunningVideo = true;
                }

                isEnabled = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //RunVideo();
        }

        protected override void OnDisappearing()
        {
            //RunVideo();
            base.OnDisappearing();
        }
    }
}