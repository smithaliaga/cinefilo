namespace cinefilo.Util {

    using Plugin.Permissions;
    using Plugin.Permissions.Abstractions;
    using System;
    using System.Threading.Tasks;
    using Permission = Plugin.Permissions.Abstractions.Permission;

    public class Permissions {
        public static async Task<bool> GetPermissions(Permission permission) {
            bool stateEvent = false;
            try {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                if (status != PermissionStatus.Granted) {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission)) {
                        //await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(permission))
                        status = results[permission];
                }

                if (status == PermissionStatus.Granted) {
                    stateEvent = true;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return stateEvent;
        }
    }

    
}
