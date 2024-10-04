mergeInto(LibraryManager.library, {
    CreateUserWithEmailAndPassword: function (email, password) {
        var parsedEmail = UTF8ToString(email);
        var parsedPassword = UTF8ToString(password);

        try {
            firebase.auth().createUserWithEmailAndPassword(parsedEmail, parsedPassword)
                .then(function (user) {
                    console.log("Register Success");
                    window.unityInstance.SendMessage("Register", "RegisterUser", 1);
                })
                .catch ((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log("Register Failed: " + errorMessage);
                    window.unityInstance.SendMessage("Register", "RegisterUser", 0);
                });

        } catch (error) {
            console.log(error);
            window.unityInstance.SendMessage("Register", "RegisterUser", 0);
        }
    },

    SignInWithEmailAndPassword: function (email, password) {
        var parsedEmail = UTF8ToString(email);
        var parsedPassword = UTF8ToString(password);
        alert("SignInWithEmailAndPassword Called");

        try {
            firebase.auth().signInWithEmailAndPassword(parsedEmail, parsedPassword)
                .then(function (user) {
                    console.log("Login Success");
                    window.unityInstance.SendMessage("LoginManager", "AuthenticateUser", 1);
                })
                .catch ((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log("Login Failed: " + errorMessage);
                    window.unityInstance.SendMessage("LoginManager", "AuthenticateUser", 0);
                });
        } catch (error) {
            console.log(error);
            window.unityInstance.SendMessage("LoginManager", "AuthenticateUser", 0);
        }
    },

    TestCallBack: function() {
        window.unityInstance.SendMessage("LoginManager", "AuthenticateUser", 256);
    }
});
