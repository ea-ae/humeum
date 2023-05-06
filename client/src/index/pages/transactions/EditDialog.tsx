import * as Mui from '@mui/material';
import { useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import * as React from 'react';

import { TransactionDto } from '../../api/api';
import CurrencyInput from '../../components/cards/CurrencyInput';
import Input from '../../components/cards/Input';

interface Props {
  transaction: TransactionDto | null; // transaction remains after closing for the transition
  isOpen: boolean;
  onSave: (transaction: TransactionDto) => void;
}

export default function EditDialog({ transaction, isOpen, onSave }: Props) {
  if (transaction === null) return null;

  const [data, setData] = React.useState<TransactionDto>(transaction);

  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));

  const namePattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,50}/;
  const descriptionPattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,400}/;

  return (
    <Mui.Dialog open={isOpen} onClose={onSave} fullScreen={fullScreen} classes={{ paper: 'md:min-w-[60vw] lg:min-w-[30vw]' }}>
      <Mui.DialogTitle>Edit transaction #{transaction.id}</Mui.DialogTitle>
      <Mui.DialogContent dividers>
        <div className="grid grid-cols-1 gap-4 lg:grid-cols-2 2xl:grid-cols-3">
          <Input
            label="Name"
            defaultValue={transaction.name}
            typePattern={namePattern}
            validPattern={namePattern}
            className="2xl:col-span-2"
            isOutlined
            onChange={(value: string) => setData(new TransactionDto({ ...data, name: value }))}
          />
          <CurrencyInput
            label="Amount"
            defaultValue={transaction.amount.toString()}
            isOutlined
            onChange={(value: number) => setData(new TransactionDto({ ...data, amount: value }))}
          />
          <Input
            label="Description"
            defaultValue={transaction.description}
            typePattern={descriptionPattern}
            validPattern={descriptionPattern}
            className="grow-1 lg:col-span-2 2xl:col-span-3"
            isOutlined
            onChange={(value: string) => setData(new TransactionDto({ ...data, description: value }))}
          />
        </div>
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={(_) => onSave(data)}>Save</Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
