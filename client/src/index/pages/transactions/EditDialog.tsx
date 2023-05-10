import * as Mui from '@mui/material';
import { useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import dayjs from 'dayjs';
import * as React from 'react';

import { AssetDto, BriefRelatedResourceDto, TaxSchemeDto, TransactionDto } from '../../api/api';
import fetchTaxSchemes from '../../api/fetchTaxSchemes';
import CurrencyInput from '../../components/cards/CurrencyInput';
import Input from '../../components/cards/Input';
import PositiveIntegerInput from '../../components/cards/PositiveIntegerInput';
import useCache, { CacheKey } from '../../hooks/useCache';

// eslint bug:
// eslint-disable-next-line no-shadow
enum EditDialogTab {
  SINGLE_TRANSACTION,
  RECURRING_TRANSACTION,
}

type TimeUnit = 'DAYS' | 'WEEKS' | 'MONTHS' | 'YEARS';

interface Props {
  transaction: TransactionDto; // transaction remains after closing for the transition
  isOpen: boolean;
  onSave: (transaction: TransactionDto) => void;
}

export default function EditDialog({ transaction, isOpen, onSave }: Props) {
  const [taxSchemes, setTaxSchemes] = useCache<TaxSchemeDto[] | null>(CacheKey.TaxSchemes, null);
  fetchTaxSchemes(taxSchemes, setTaxSchemes);

  const [assets, setAssets] = useCache<AssetDto[] | null>(CacheKey.Assets, null);

  const [data, setData] = React.useState<TransactionDto>(transaction);
  const [selectedTaxScheme, setSelectedTaxScheme] = React.useState<number>(transaction.taxScheme.id);
  const [selectedAsset, setSelectedAsset] = React.useState<number>(transaction.asset?.id ?? -1);
  const [selectedCategories, setSelectedCategories] = React.useState<number[]>(transaction.categories.map((c) => c.id));

  const [activeTab, setActiveTab] = React.useState<number>(EditDialogTab.SINGLE_TRANSACTION);

  React.useEffect(() => {
    setData(transaction);
    setSelectedTaxScheme(transaction.taxScheme.id);
    setSelectedAsset(transaction.asset?.id ?? -1);
    setSelectedCategories(transaction.categories.map((c) => c.id));
    setActiveTab(EditDialogTab.SINGLE_TRANSACTION);
  }, [transaction.id]); // whenever the default value changes, reset the state

  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));

  const namePattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,50}/;
  const descriptionPattern = /[A-Za-z0-9ÕÄÖÜõäöü ]{0,400}/;

  if (taxSchemes === null) {
    return 'Loading...';
  }

  const singleTransactionTab = (
    <>
      <Input
        label="Name"
        defaultValue={data.name}
        typePattern={namePattern}
        validPattern={namePattern}
        className="md:col-span-2"
        isOutlined
        onChange={(value: string) => setData(new TransactionDto({ ...data, name: value }))}
      />
      <CurrencyInput
        label="Amount"
        defaultValue={data.amount}
        isOutlined
        onChange={(value: number) => setData(new TransactionDto({ ...data, amount: value }))}
      />
      <Input
        label="Description"
        defaultValue={data.description ?? ''}
        typePattern={descriptionPattern}
        validPattern={descriptionPattern}
        className="md:col-span-2"
        isOutlined
        onChange={(value: string) => setData(new TransactionDto({ ...data, description: value }))}
      />
      <DatePicker
        label="Start date"
        defaultValue={dayjs(data.paymentTimelinePeriodStart)}
        className="my-2"
        onChange={(value) => setData(new TransactionDto({ ...data, paymentTimelinePeriodStart: (value as dayjs.Dayjs).toDate() }))}
      />
      <Mui.Select
        value={data.taxScheme.id}
        className="my-2"
        onChange={(event) =>
          setData(
            new TransactionDto({
              ...data,
              taxScheme: new BriefRelatedResourceDto({
                id: event.target.value as number,
                name: taxSchemes[event.target.value as number].name,
              }),
            })
          )
        }
      >
        {taxSchemes?.map((taxScheme) => (
          <Mui.MenuItem value={taxScheme.id} key={taxScheme.id}>
            {taxScheme.name}
          </Mui.MenuItem>
        ))}
      </Mui.Select>
      <Mui.Select value={selectedAsset} className="my-2" onChange={(event) => setSelectedAsset(event.target.value as number)}>
        <Mui.MenuItem value={-1}>No asset</Mui.MenuItem>
        <Mui.MenuItem value={1}>III pillar, pre-2021</Mui.MenuItem>
      </Mui.Select>
      <Mui.Select
        value={selectedCategories}
        multiple
        displayEmpty
        className="my-2"
        onChange={(event) => setSelectedCategories(event.target.value as number[])}
        renderValue={(selected) => (selected.length === 0 ? 'No categories' : `${selected.length} categories`)}
      >
        <Mui.MenuItem value={1}>Food</Mui.MenuItem>
        <Mui.MenuItem value={2}>Rent</Mui.MenuItem>
        <Mui.MenuItem value={3}>Lifestyle</Mui.MenuItem>
      </Mui.Select>
    </>
  );

  const recurringTransactionTab = (
    <>
      <PositiveIntegerInput
        label="n times"
        defaultValue={data.paymentTimelineFrequencyTimesPerCycle ?? null}
        onChange={(n) => setData(new TransactionDto({ ...data, paymentTimelineFrequencyTimesPerCycle: n ?? undefined }))}
      />
      <PositiveIntegerInput
        label="every n"
        defaultValue={data.paymentTimelineFrequencyUnitsInCycle ?? null}
        onChange={(n) => setData(new TransactionDto({ ...data, paymentTimelineFrequencyUnitsInCycle: n ?? undefined }))}
      />
      <Mui.Select
        value={data.paymentTimelineFrequencyTimeUnitCode ?? ''}
        className="my-2"
        onChange={(event) => setData(new TransactionDto({ ...data, paymentTimelineFrequencyTimeUnitCode: event.target.value as TimeUnit }))}
      >
        <Mui.MenuItem value="DAYS">Days</Mui.MenuItem>
        <Mui.MenuItem value="WEEKS">Weeks</Mui.MenuItem>
        <Mui.MenuItem value="MONTHS">Months</Mui.MenuItem>
        <Mui.MenuItem value="YEARS">Years</Mui.MenuItem>
      </Mui.Select>
      <DatePicker
        label="Start date"
        defaultValue={dayjs(data.paymentTimelinePeriodStart)}
        className="md:col-span-3 my-2"
        onChange={(value) => setData(new TransactionDto({ ...data, paymentTimelinePeriodStart: (value as dayjs.Dayjs).toDate() }))}
      />
      <DatePicker
        label="End date"
        defaultValue={data.paymentTimelinePeriodEnd === undefined ? null : dayjs(data.paymentTimelinePeriodEnd)}
        className="md:col-span-3 my-2"
        onChange={(value) => setData(new TransactionDto({ ...data, paymentTimelinePeriodEnd: (value as dayjs.Dayjs).toDate() }))}
      />
    </>
  );

  const onDialogClose = () => {
    if (activeTab === EditDialogTab.SINGLE_TRANSACTION) {
      const singleTransaction = new TransactionDto({
        ...data,
        paymentTimelinePeriodEnd: undefined,
        paymentTimelineFrequencyTimesPerCycle: undefined,
        paymentTimelineFrequencyUnitsInCycle: undefined,
        paymentTimelineFrequencyTimeUnitCode: undefined,
        paymentTimelineFrequencyTimeUnitName: undefined,
      });
      onSave(singleTransaction);
    } else {
      onSave(data);
    }
  };

  return (
    <Mui.Dialog open={isOpen} onClose={onDialogClose} fullScreen={fullScreen} classes={{ paper: 'md:min-w-[60vw] lg:min-w-[30vw]' }}>
      <Mui.DialogContent dividers>
        <Mui.Tabs value={activeTab} centered={!fullScreen} variant="fullWidth" onChange={(_, value) => setActiveTab(value)}>
          <Mui.Tab label="Single" />
          <Mui.Tab label="Recurring" />
        </Mui.Tabs>
        <Mui.Divider />
        <div className="grid grid-cols-1 md:grid-cols-3 gap-x-4 gap-y-1 my-4">
          {activeTab === EditDialogTab.SINGLE_TRANSACTION ? singleTransactionTab : null}
          {activeTab === EditDialogTab.RECURRING_TRANSACTION ? recurringTransactionTab : null}
        </div>
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={onDialogClose}>
          {activeTab === EditDialogTab.SINGLE_TRANSACTION ? 'Save as a single-time transaction' : null}
          {activeTab === EditDialogTab.RECURRING_TRANSACTION ? 'Save as a recurring transaction' : null}
        </Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
