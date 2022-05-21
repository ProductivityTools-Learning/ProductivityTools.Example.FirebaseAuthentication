// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";

import {
    GoogleAuthProvider,
    getAuth,
    signInWithPopup,
    signOut,
} from "firebase/auth";

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyDgSHqUdtL0XQ1i95Y_fMKTHV48Yjo_ZWs",
  authDomain: "example-firebaseauthenticaton.firebaseapp.com",
  projectId: "example-firebaseauthenticaton",
  storageBucket: "example-firebaseauthenticaton.appspot.com",
  messagingSenderId: "1089727478236",
  appId: "1:1089727478236:web:61b79fcc0a666e33907719"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);

const auth = getAuth(app);

const googleProvider = new GoogleAuthProvider();
const signInWithGoogle = async () => {
    try {
        debugger;
        const res = await signInWithPopup(auth, googleProvider);
        console.log(res);
    } catch (err) {
        console.error(err);
        alert(err.message);
    }
};

const logout = () => {
    signOut(auth);
    
};


export {
    auth,
    signInWithGoogle,
    logout,
};