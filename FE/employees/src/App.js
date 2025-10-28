import "./App.css";

import MainPanel from "./components/main_panel";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>Pair of employees who have worked together</h1>
        <p>
          Let's identify the pair of employees who have worked together on
          common projects for the longest period of time.
        </p>
        <p>Please upload a file and submit the form.</p>
        <MainPanel />
      </header>
    </div>
  );
}

export default App;
