import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";

const Result = ({ employees }) => {
  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }}>
        <TableHead>
          <TableRow>
            <TableCell>Employee ID #1</TableCell>
            <TableCell>Employee ID #2</TableCell>
            <TableCell>Project ID</TableCell>
            <TableCell>Days worked</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {employees?.map((emp) => (
            <TableRow>
              <TableCell>{emp.employee1Id}</TableCell>
              <TableCell>{emp.employee2Id}</TableCell>
              <TableCell>{emp.projectId}</TableCell>
              <TableCell>{emp.daysTogether}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default Result;
