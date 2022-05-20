import apiService from "./apiService";
import {useState} from 'react'

function App() {

  const [date, setDate] = useState([])
  const [protectedDate, setProtectedDate] = useState([])

  const getDate = async () => {
    let x = await apiService.getDate();
    setDate(x);
  }

  const getProtectedDate=() => {
    let x = apiService.getProtectedDate();
    setProtectedDate(x);
  }

  return (
    <div className="App">
      <button onClick={getDate}>GetDate</button> <span>{date}</span>
      <button onClick={getProtectedDate}>GetDate</button> <span>{protectedDate}</span>
    </div>
  );
}

export default App;
