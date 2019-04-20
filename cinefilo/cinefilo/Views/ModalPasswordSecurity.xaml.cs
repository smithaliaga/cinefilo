namespace cinefilo.Views {
    using cinefilo.Util;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ModalPasswordSecurity : Rg.Plugins.Popup.Pages.PopupPage
    {
        private Entry entryOrigin;

        public ModalPasswordSecurity(Entry entry, string textTitle, string textButton)
        {
            this.entryOrigin = entry;
            InitializeComponent();
            CloseWhenBackgroundIsClicked = false;

            this.LabelTitle.Text = textTitle;
            this.btnAccept.Text = textButton;
            this.entryOrigin.Text = "";

            if (!string.IsNullOrEmpty(entryOrigin.Text))
            {
                for (int i = 0; i < entryOrigin.Text.Length; i++)
                {
                    this.Keyboard.Text += "*";
                }
            }

            //DEFINIMOS LA CANTIDAD DE FILAS EN EL GRID
            int countRowMax = 4;
            RowDefinitionCollection rowDefinitions = new RowDefinitionCollection();
            for (int i = 0; i < countRowMax; i++)
            {
                rowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            //DEFINIMOS LA CANTIDAD DE COLUMNAS EN EL GRID
            int countColumnMax = 3;
            ColumnDefinitionCollection columnDefinitions = new ColumnDefinitionCollection();
            for (int i = 0; i < countColumnMax; i++)
            {
                columnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            //CREAMOS EL GRID
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = rowDefinitions,
                ColumnDefinitions = columnDefinitions,
                ColumnSpacing = 20,
                RowSpacing = 20,
            };

            //CARGAMOS LOS BOTONES EN EL GRID
            int countMaxNumberRandom = 10;
            List<int> listNumbers = GenerateRandom(countMaxNumberRandom, 0, countMaxNumberRandom);
            int countNumber = 0;
            int countNumberRepeat = 0;

            //RECORREMOS LAS FILAS
            for (int row = 0; row < countRowMax; row++)
            {
                //RECORREMOS LAS COLUMNAS
                for (int col = 0; col < countColumnMax; col++)
                {
                    try
                    {
                        int numberRamdom = listNumbers[countNumber];

                        if (countNumber == 9)
                        {
                            ///PERMITE COLOCAR LOS BOTONES EN LA ULTIMA FILA
                            if (countNumberRepeat == 0)
                            {
                                var btn = CreateButton("C");
                                btn.BackgroundColor = Color.FromHex("#eeeeee");
                                grid.Children.Add(btn, col, row);
                            }
                            else if (countNumberRepeat == 1)
                            {
                                var btn = CreateButton(numberRamdom.ToString());
                                grid.Children.Add(btn, col, row);
                            }
                            else if (countNumberRepeat == 2)
                            {
                                var btn = CreateButton("X");
                                btn.BackgroundColor = Color.FromHex("#eeeeee");
                                grid.Children.Add(btn, col, row);
                            }
                            countNumberRepeat++;
                            continue;
                        }
                        else
                        {
                            var btn = CreateButton(numberRamdom.ToString());
                            grid.Children.Add(btn, col, row);
                        }
                    }
                    catch(ArgumentOutOfRangeException)
                    {

                    }

                    countNumber++;
                }
            }

            CustomButtons.Children.Add(grid);
        }

        private Button CreateButton(string text)
        {
            var btn = new Button()
            {
                Text = text,
                FontSize = 24,
                StyleId = AES.Encrypt(text, Globales.ENCRYPTION_KEY),
                WidthRequest = 60,
                HeightRequest = 60,
                CornerRadius = 30,
                BorderWidth = 1,
                BorderColor = Color.Orange,
                BackgroundColor = Color.White,
            };
            btn.Clicked += OnDynamicBtnClicked;
            return btn;
        }

        private void OnDynamicBtnClicked(object sender, EventArgs e)
        {
            int lengthEntryOrigin = entryOrigin.Text.Length;
            var myBtn = sender as Button;
            var myId = AES.Decrypt(myBtn.StyleId, Globales.ENCRYPTION_KEY);

            if (myId == "X")
            {
                if (lengthEntryOrigin > 0)
                {
                    entryOrigin.Text = entryOrigin.Text.Substring(0, lengthEntryOrigin - 1);
                    this.Keyboard.Text = this.Keyboard.Text.Substring(0, lengthEntryOrigin - 1);
                }
            }
            else if (myId == "C")
            {
                if (lengthEntryOrigin > 0)
                {
                    entryOrigin.Text = "";
                    this.Keyboard.Text = "";
                }
            }
            else
            {
                if (lengthEntryOrigin < entryOrigin.MaxLength)
                {
                    entryOrigin.Text = entryOrigin.Text + myId;
                    this.Keyboard.Text += "*";

                    if (lengthEntryOrigin == entryOrigin.MaxLength - 1)
                    {
                        PopupNavigation.Instance.PopAsync();
                    }
                }
            }
        }

        public async void OnAccept(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        static Random random = new Random();

        static List<int> GenerateRandom(int count, int min, int max)
        {

            //  initialize set S to empty
            //  for J := N-M + 1 to N do
            //    T := RandInt(1, J)
            //    if T is not in S then
            //      insert T in S
            //    else
            //      insert J in S
            //
            // adapted for C# which does not have an inclusive Next(..)
            // and to make it from configurable range not just 1.

            if (max <= min || count < 0 ||
                    // max - min > 0 required to avoid overflow
                    (count > max - min && max - min > 0))
            {
                // need to use 64-bit to support big ranges (negative min, positive max)
                throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
                        " (" + ((Int64)max - (Int64)min) + " values), or count " + count + " is illegal");
            }

            // generate count random values.
            HashSet<int> candidates = new HashSet<int>();

            // start count values before max, and end at max
            for (int top = max - count; top < max; top++)
            {
                // May strike a duplicate.
                // Need to add +1 to make inclusive generator
                // +1 is safe even for MaxVal max value because top < max
                if (!candidates.Add(random.Next(min, top + 1)))
                {
                    // collision, add inclusive max.
                    // which could not possibly have been added before.
                    candidates.Add(top);
                }
            }

            // load them in to a list, to sort
            List<int> result = candidates.ToList();

            // shuffle the results because HashSet has messed
            // with the order, and the algorithm does not produce
            // random-ordered results (e.g. max-1 will never be the first value)
            for (int i = result.Count - 1; i > 0; i--)
            {
                int k = random.Next(i + 1);
                int tmp = result[k];
                result[k] = result[i];
                result[i] = tmp;
            }
            return result;
        }

        static List<int> GenerateRandom(int count)
        {
            return GenerateRandom(count, 0, Int32.MaxValue);
        }

        protected override void OnAppearing()  {
            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

        protected override void OnAppearingAnimationBegin() {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd() {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin() {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd() {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()  {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync() {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync() {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()  {
            return base.OnDisappearingAnimationEndAsync();
        }

        protected override bool OnBackButtonPressed() {
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked() {
            return base.OnBackgroundClicked();
        }
    }
}