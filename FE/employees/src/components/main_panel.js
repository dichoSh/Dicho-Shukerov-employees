import { useState } from "react";
import Result from "./result";
import UploadForm from "./upload";
import { Grid } from "@mui/material";
import config from "../config.json";

const MainPanel = () => {
  const [employees, setEmployees] = useState([]);
  const [error, setError] = useState(null);

  const submit = async (file) => {
    if (!file) return;

    var formData = new FormData();
    formData.append("file", file);

    await fetch(`${config.API}/Employees/Upload`, {
      method: "POST",
      body: formData,
    })
      .then((x) => {
        if (x.status === 200) return x.json();

        x.text().then((err) => {
          setError(err);
          setEmployees([]);
        });
      })
      .then((data) => {
        setEmployees(data);
        setError(null);
      })
      .catch((e) => {
        setError(e.message);
        setEmployees([]);
      });
  };

  return (
    <Grid rowSpacing={40}>
      <Grid>
        <UploadForm submit={submit} />
      </Grid>
      <Grid>{error && <p style={{ color: "red" }}>{error}</p>}</Grid>
      <Grid>
        <Result employees={employees} />
      </Grid>
    </Grid>
  );
};

export default MainPanel;
