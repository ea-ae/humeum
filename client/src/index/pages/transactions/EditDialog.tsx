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
  const [selectedTaxScheme, setSelectedTaxScheme] = React.useState<number>(transaction.taxScheme.id);
  const [selectedAsset, setSelectedAsset] = React.useState<number>(transaction.asset?.id ?? -1);
  const [selectedCategories, setSelectedCategories] = React.useState<number[]>(transaction.categories.map((c) => c.id));

  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));

  const namePattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,50}/;
  const descriptionPattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,400}/;

  const singleTransactionTab = (
    <>
      <Input
        label="Name"
        defaultValue={transaction.name}
        typePattern={namePattern}
        validPattern={namePattern}
        className="lg:col-span-2"
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
        className="lg:col-span-2"
        isOutlined
        onChange={(value: string) => setData(new TransactionDto({ ...data, description: value }))}
      />
      <DatePicker label="Start date" className="my-2" />
      <Mui.Select value={selectedTaxScheme} className="my-2" onChange={(event) => setSelectedTaxScheme(event.target.value as number)}>
        <Mui.MenuItem value={1}>Income tax</Mui.MenuItem>
        <Mui.MenuItem value={2}>Value added tax</Mui.MenuItem>
      </Mui.Select>
      <Mui.Select value={selectedAsset} className="my-2" onChange={(event) => setSelectedAsset(event.target.value as number)}>
        <Mui.MenuItem value={-1}>No asset</Mui.MenuItem>
        <Mui.MenuItem value={1}>III pillar, pre-2021</Mui.MenuItem>
      </Mui.Select>
      <Mui.Select
        value={selectedCategories}
        multiple
        className="my-2"
        onChange={(event) => setSelectedCategories(event.target.value as number[])}
      >
        <Mui.MenuItem value={1}>Food</Mui.MenuItem>
        <Mui.MenuItem value={2}>Rent</Mui.MenuItem>
        <Mui.MenuItem value={3}>Lifestyle</Mui.MenuItem>
      </Mui.Select>
    </>
  );

  const recurringTransactionTab = <></>;

  return (
    <Mui.Dialog open={isOpen} onClose={onSave} fullScreen={fullScreen} classes={{ paper: 'md:min-w-[60vw] lg:min-w-[30vw]' }}>
      <Mui.DialogContent dividers>
        <Mui.Tabs value={activeTab} centered={!fullScreen} variant="fullWidth" onChange={(_, value) => setActiveTab(value)}>
          <Mui.Tab label="Single" />
          <Mui.Tab label="Recurring" />
        </Mui.Tabs>
        <Mui.Divider />
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-x-4 gap-y-1 my-4">
          {activeTab === 0 ? singleTransactionTab : null}
          {activeTab === 1 ? recurringTransactionTab : null}
        </div>
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={(_) => onSave(data)}>Save as One-Time Transaction</Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
