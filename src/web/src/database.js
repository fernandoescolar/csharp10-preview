import firebase from 'firebase'

const firebaseConfig = {
    apiKey: "xxx",
    authDomain: "xxxx.firebaseapp.com",
    projectId: "xxxxx",
    storageBucket: "xxxxxx.appspot.com",
    messagingSenderId: "xxxxxxx",
    appId: "xxxxxxxx"
  };

// Get a Firestore instance
export const db = firebase
  .initializeApp(firebaseConfig)
  .firestore();
