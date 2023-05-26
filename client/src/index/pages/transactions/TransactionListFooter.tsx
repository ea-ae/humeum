import * as Mui from '@mui/material';
import { GridFooter, GridFooterContainer } from '@mui/x-data-grid';

interface Props {
  onCreateClick: () => void;
}

export default function TransactionListFooter({ onCreateClick }: Props) {
  return (
    <GridFooterContainer>
      <Mui.Button variant="outlined" onClick={onCreateClick} className="mx-4 whitespace-nowrap">
        Create new
      </Mui.Button>
      <GridFooter />
    </GridFooterContainer>
  );
}
