package Core;

/*Singleton to keep track of Current User Context*/
public class AndroidContext {
    private static AndroidContext ourInstance = new AndroidContext();
    private Profile currentProfile = new Profile();

    private AndroidContext() {
    }

    public static AndroidContext getInstance() {
        return ourInstance;
    }

    public Profile getCurrentProfile() {
        return this.currentProfile;
    }

    public void setCurrentProfile(Profile p) {
        this.currentProfile = p;
    }
}
