import apiService from "./apiService";
import { useState } from 'react'
import Login from './login'

function App() {

  const [date, setDate] = useState([])
  const [protectedDate, setProtectedDate] = useState([])

  const getDate = async () => {
    let x = await apiService.getDate();
    setDate(x);
  }

  const getProtectedDate = async () => {
    let x = await apiService.getProtectedDate();
    setProtectedDate(x);
  }

  return (
    <div className="App">
      <button onClick={getDate}>GetDate</button> <span>{date}</span><br />
      <button onClick={getProtectedDate}>GetDate Protected</button> <span>{protectedDate}</span>
      <Login />
    </div>
  );
}

export default App;
