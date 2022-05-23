import axios from 'axios'
import { auth } from './firebase'

async function getDate() {
    const response = await axios.get('http://127.0.0.1:8080/Date');
    console.log(response);
    return response.data;
}

async function getProtectedDate() {

    const header = {
        headers: { Authorization: `Bearer ${auth.currentUser.accessToken}` }
    }

    const response = await axios.get('http://127.0.0.1:8080/ProtectedDate', header);
    console.log(response);
    return response.data;
}

export default {
    getDate,
    getProtectedDate
}