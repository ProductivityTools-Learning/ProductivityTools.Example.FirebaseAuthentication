import axios from 'axios'

async function getDate(){
    const response=await axios.get('http://127.0.0.1:8080/Date');
    console.log(response);
    return response.data;
}

async function getProtectedDate(){
    const response=await axios.get('http://127.0.0.1:8080/ProtectedDate');
    console.log(response);
    return response.data;
}

export default {
    getDate,
    getProtectedDate
}