namespace RecruitingStaff.WebApp.ViewModels
{
    public class BaseViewModel
    {
        public BaseViewModel()
        {

        }

        public BaseViewModel(object entity)
        {
            foreach (var type in entity.GetType().GetProperties())
            {
                var property = GetType().GetProperty(type.Name, type.PropertyType);
                if (property != null)
                {
                    property.SetValue(this, type.GetValue(entity));
                }
            }
        }
    }
}
