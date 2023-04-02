import * as Mui from '@mui/material';
import { GridFooter, GridFooterContainer } from '@mui/x-data-grid';

export default function TransactionListFooter() {
  return (
    <GridFooterContainer>
      <Mui.Button variant="outlined" className="mx-4 whitespace-nowrap">
        Create new
      </Mui.Button>
      <GridFooter />
    </GridFooterContainer>
  );
}