import * as Mui from '@mui/material';
import { useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import * as React from 'react';

import { TransactionDto } from '../../api/api';
import Input from '../../components/cards/Input';

interface Props {
  transaction: TransactionDto | null; // transaction remains after closing for the transition
  isOpen: boolean;
  onClose: () => void;
}

export default function EditDialog({ transaction, isOpen, onClose }: Props) {
  const theme = useTheme();

  if (transaction === null) return null;

  const namePattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,50}/;
  const descriptionPattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,400}/;
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));

  return (
    <Mui.Dialog open={isOpen} onClose={onClose} fullScreen={fullScreen} classes={{ paper: 'md:min-w-[60vw] lg:min-w-[30vw]' }}>
      <Mui.DialogTitle>Edit transaction #{transaction.id}</Mui.DialogTitle>
      <Mui.DialogContent dividers>
        <Input
          label="Name"
          defaultValue={transaction.name}
          typePattern={namePattern}
          validPattern={namePattern}
          variant="outlined"
          className="my-4"
        />
        <Input
          label="Description"
          defaultValue={transaction.description}
          typePattern={descriptionPattern}
          validPattern={descriptionPattern}
          variant="outlined"
          className="my-4"
        />
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={onClose}>Cancel</Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
