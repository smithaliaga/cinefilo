namespace cinefilo.Models.Local
{
    public class EntityModalPicker
    {
        public object Code { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool EnableIcon { get; set; }
        public object ObjectEntity { get; set; }

        public EntityModalPicker()
        {

        }

        public EntityModalPicker(object Code, string Name)
        {
            this.Code = Code;
            this.Name = Name;
        }

        public EntityModalPicker(object Code, string Name, object ObjectEntity)
        {
            this.Code = Code;
            this.Name = Name;
            this.ObjectEntity = ObjectEntity;
        }

        public EntityModalPicker(object Code, string Name, string Icon, bool EnableIcon)
        {
            this.Code = Code;
            this.Name = Name;
            this.Icon = Icon;
            this.EnableIcon = EnableIcon;
        }
    }
}
