mergeInto(LibraryManager.library, {

    CreateUserWithEmailAndPassword: function (email, password, objectName, callback, fallback) {
        var parsedEmail = Pointer_stringify(email);
        var parsedPassword = Pointer_stringify(password);
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        try {

            firebase.auth().createUserWithEmailAndPassword(parsedEmail, parsedPassword).then(function (unused) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "Success: signed up for " + parsedEmail);
            }).catch(function (error) {
                window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });

        } catch (error) {
            window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

    SignInWithEmailAndPassword: function (email, password, objectName, callback, fallback) {
        var parsedEmail = Pointer_stringify(email);
        var parsedPassword = Pointer_stringify(password);
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        try {

            firebase.auth().signInWithEmailAndPassword(parsedEmail, parsedPassword).then(function (unused) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "Success: signed in for " + parsedEmail);
            }).catch(function (error) {
                window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });

        } catch (error) {
            window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

    CurrentUser: function (objectName, callback, fallback) {
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        try {
            
            var cUser = firebase.auth().currentUser;
            if (cUser) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, JSON.stringify(cUser));
            } else {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "");
            }

        } catch (error) {
            window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

    SignInWithGoogle: function (objectName, callback, fallback) {
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        try {
            var provider = new firebase.auth.GoogleAuthProvider();
            firebase.auth().signInWithRedirect(provider).then(function (unused) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "Success: signed in with Google!");
            }).catch(function (error) {
                window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });

        } catch (error) {
            window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

    SignInWithFacebook: function (objectName, callback, fallback) {
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);

        try {
            var provider = new firebase.auth.FacebookAuthProvider();
            firebase.auth().signInWithRedirect(provider).then(function (unused) {
                window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "Success: signed in with Facebook!");
            }).catch(function (error) {
                window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
            });

        } catch (error) {
            window.unityInstance.SendMessage(parsedObjectName, parsedFallback, JSON.stringify(error, Object.getOwnPropertyNames(error)));
        }
    },

    OnAuthStateChanged: function (objectName, onUserSignedIn, onUserSignedOut) {
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedOnUserSignedIn = Pointer_stringify(onUserSignedIn);
        var parsedOnUserSignedOut = Pointer_stringify(onUserSignedOut);

        firebase.auth().onAuthStateChanged(function(user) {
            if (user) {
                window.unityInstance.SendMessage(parsedObjectName, parsedOnUserSignedIn, JSON.stringify(user));
            } else {
                window.unityInstance.SendMessage(parsedObjectName, parsedOnUserSignedOut, "User signed out");
            }
        });

    },

    SignOut: function (objectName, callback, fallback) {
        var parsedObjectName = Pointer_stringify(objectName);
        var parsedCallback = Pointer_stringify(callback);
        var parsedFallback = Pointer_stringify(fallback);
        firebase.auth().signOut().then(function() {
            window.unityInstance.SendMessage(parsedObjectName, parsedCallback, "User signed out");
        }).catch(function(error) {
            window.unityInstance.SendMessage(parsedObjectName, parsedFallback, "Something went wrong when sign out user!");
        })
    }
});