import * as Mui from '@mui/material';
import { useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import * as React from 'react';

import { TransactionDto } from '../../api/api';
import CurrencyInput from '../../components/cards/CurrencyInput';
import Input from '../../components/cards/Input';

interface Props {
  transaction: TransactionDto; // transaction remains after closing for the transition
  isOpen: boolean;
  onSave: (transaction: TransactionDto) => void;
}

export default function EditDialog({ transaction, isOpen, onSave }: Props) {
  const [data, setData] = React.useState<TransactionDto>(transaction);
  const [activeTab, setActiveTab] = React.useState<number>(0);

  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));

  const namePattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,50}/;
  const descriptionPattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,400}/;

  return (
    <Mui.Dialog open={isOpen} onClose={onSave} fullScreen={fullScreen} classes={{ paper: 'md:min-w-[60vw] lg:min-w-[30vw]' }}>
      <Mui.DialogTitle>Edit transaction #{transaction.id}</Mui.DialogTitle>
      <Mui.DialogContent dividers>
        <Mui.Tabs value={activeTab} centered onChange={(_, value) => setActiveTab(value)}>
          <Mui.Tab label="Single" />
          <Mui.Tab label="Recurring" />
        </Mui.Tabs>
        <div className="grid grid-cols-1 gap-x-4 gap-y-2 lg:grid-cols-2 2xl:grid-cols-3">
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
            defaultValue={transaction.amount}
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
          <DatePicker label="Start date" className="my-2" />
        </div>
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={(_) => onSave(data)}>Save</Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
