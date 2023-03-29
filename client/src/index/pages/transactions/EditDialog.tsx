import * as Mui from '@mui/material';
import * as React from 'react';

import { TransactionDto } from '../../api/api';

interface Props {
  transaction: TransactionDto | null; // transaction remains after closing for the transition
  isOpen: boolean;
  onClose: () => void;
}

export default function EditDialog({ transaction, isOpen, onClose }: Props) {
  return (
    <Mui.Dialog open={isOpen} onClose={onClose}>
      <Mui.DialogTitle>Edit transaction {transaction?.id}</Mui.DialogTitle>
    </Mui.Dialog>
  );
}
