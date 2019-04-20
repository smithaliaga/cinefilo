namespace cinefilo.Util
{
    using System;
    using Xamarin.Forms;

    public class CustomEntry : Entry
    {
        /********************RETURN TYPE*****************/
        public CustomEntry()
        {
            Completed += Goto;
        }

        public CustomEntry Next { get; set; }

        private void Goto(object sender, EventArgs e)
        {
            if (sender != null && ((CustomEntry)sender).Next != null)
                ((CustomEntry)sender).Next.Focus();
        }

        // Need to overwrite default handler because we cant Invoke otherwise
        public new event EventHandler Completed;

        public new static readonly BindableProperty ReturnTypeProperty = 
            BindableProperty.Create(nameof(ReturnType),typeof(EnumReturnType),typeof(CustomEntry),EnumReturnType.Done,BindingMode.OneWay
        );

        public new EnumReturnType ReturnType
        {
            get { return (EnumReturnType)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }

        /********************IMAGE*****************/

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomEntry), string.Empty);

        public static readonly BindableProperty LineColorProperty =
            BindableProperty.Create(nameof(LineColor), typeof(Xamarin.Forms.Color), typeof(CustomEntry), Color.White);

        public static readonly BindableProperty ImageHeightProperty =
            BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(CustomEntry), Util.GetIconSizeDefault());

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(CustomEntry), Util.GetIconSizeDefault());

        public static readonly BindableProperty ImageAlignmentProperty =
            BindableProperty.Create(nameof(ImageAlignment), typeof(EnumImageAlignment), typeof(CustomEntry), EnumImageAlignment.Left);

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public EnumImageAlignment ImageAlignment
        {
            get { return (EnumImageAlignment)GetValue(ImageAlignmentProperty); }
            set { SetValue(ImageAlignmentProperty, value); }
        }
    }

    public enum EnumImageAlignment
    {
        Left,
        Right
    }

    public enum EnumReturnType
    {
        Go,
        Next,
        Done,
        Send,
        Search
    }
}
