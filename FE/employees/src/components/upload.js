import { Button, FormControl, Grid, TableCell, TableRow } from "@mui/material";
import { red } from "@mui/material/colors";
import { styled } from "@mui/material/styles";
import { useState } from "react";

const StyledInput = styled("input")({
  clip: "rect(0 0 0 0)",
  clipPath: "inset(50%)",
  height: 1,
  overflow: "hidden",
  position: "absolute",
  bottom: 0,
  left: 0,
  whiteSpace: "nowrap",
  width: 1,
});

const UploadForm = ({ submit }) => {
  const [file, setFile] = useState(null);

  return (
    <>
      <FormControl>
        <Grid>
          <Button component="label" variant="contained">
            Upload file
            <StyledInput
              type="file"
              accept=".csv"
              onChange={(event) => setFile(event.target.files[0])}
            />
          </Button>
        </Grid>
        <Grid margin={2}>
          <Grid>
            {file?.name && (
              <Button
                style={{ backgroundColor: red }}
                onClick={() => submit(file)}
              >
                Submit {file?.name}
              </Button>
            )}
          </Grid>
        </Grid>
      </FormControl>
    </>
  );
};

export default UploadForm;
