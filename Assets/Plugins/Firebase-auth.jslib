mergeInto(LibraryManager.library, {
    CreateUserWithEmailAndPassword: function (email, password) {
        var parsedEmail = UTF8ToString(email);
        var parsedPassword = UTF8ToString(password);
        // var parsedObjectName = UTF8ToString(objectName);
        // var parsedCallback = UTF8ToString(callback);
        // var parsedFallback = UTF8ToString(fallback);

        try {

            firebase.auth().createUserWithEmailAndPassword(parsedEmail, parsedPassword)
                .then(function (user) {
                    // window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "Success: signed up for " + parsedEmail);
                    alert("SignUp Success for: " + user.email);
                })
                .catch ((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    // window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
                    alert("SignUp Failed: " + errorMessage);
                });

        } catch (error) {
            // window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            console.log(error);
        }
    },

    SignInWithEmailAndPassword: function (email, password) {
        var parsedEmail = UTF8ToString(email);
        var parsedPassword = UTF8ToString(password);

        try {
            firebase.auth().signInWithEmailAndPassword(parsedEmail, parsedPassword)
                .then(function (user) {
                    console.log("Login Success");
                    window.unityInstance.SendMessage("Login", "ValidateAuthentication", 1);
                })
                .catch ((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    console.log("Login Failed: " + errorMessage);
                    window.unityInstance.SendMessage("Login", "ValidateAuthentication", 0);
                });
        } catch (error) {
            console.log(error);
            window.unityInstance.SendMessage("Login", "ValidateAuthentication", 0);
        }
    },

    TestCallBack: function() {
        window.unityInstance.SendMessage("Login", "ValidateAuthentication", 256);
    }
});