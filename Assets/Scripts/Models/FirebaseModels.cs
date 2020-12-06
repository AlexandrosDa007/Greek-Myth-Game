using System;
using System.Collections.Generic;
using Scripts.GameModels;
namespace Scripts.Objects
{
    [Serializable]
    public class FirebaseUser
    {

        public string displayName;
        
        public string email;
        
        public bool isAnonymous;
        
        public bool isEmailVerified;

        public string phoneNumber;
        
        public string uid;

        public JUser user;
    }


    [Serializable]
    public class FirebaseTest
    {
        public string name;
        
        public float power;
    }
}