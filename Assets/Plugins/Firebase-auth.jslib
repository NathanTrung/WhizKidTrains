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

    SignInWithEmailAndPassword: function (email, password, objectName, callback, fallback) {
        var parsedEmail = UTF8ToString(email);
        var parsedPassword = UTF8ToString(password);
        // var parsedObjectName = UTF8ToString(objectName);
        // var parsedCallback = UTF8ToString(callback);
        // var parsedFallback = UTF8ToString(fallback);

        try {

            firebase.auth().signInWithEmailAndPassword(parsedEmail, parsedPassword)
                .then(function (user) {
                    // window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "Success: signed in for " + parsedEmail);
                    alert("Login Success for: " + user.email);
                })
                .catch ((error) => {
                    var errorCode = error.code;
                    var errorMessage = error.message;
                    // window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
                    alert("Login Failed: " + errorMessage);
                });
        } catch (error) {
            // window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            console.log(error);
        }
    }

});