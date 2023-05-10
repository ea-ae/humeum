import * as Mui from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import dayjs from 'dayjs';

import { BriefRelatedResourceDto, TaxSchemeDto, TransactionDto } from '../../api/api';
import CurrencyInput from '../../components/cards/CurrencyInput';
import Input from '../../components/cards/Input';

interface Props {
  data: TransactionDto;
  setData: (data: TransactionDto) => void;
  taxSchemes: TaxSchemeDto[];
}

export default function SingleTransactionTab({ data, setData, taxSchemes }: Props) {
  return (
    <>
      <Input
        label="Name"
        defaultValue={data.name}
        typePattern={/[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,50}/}
        validPattern={/[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{1,50}/}
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
        typePattern={/[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,400}/}
        validPattern={/[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,400}/}
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
        {taxSchemes.map((taxScheme) => (
          <Mui.MenuItem value={taxScheme.id} key={taxScheme.id}>
            {taxScheme.name}
          </Mui.MenuItem>
        ))}
      </Mui.Select>
      {/* onChange={(event) => setSelectedAsset(event.target.value as number) */}
      <Mui.Select value={1} className="my-2">
        <Mui.MenuItem value={-1}>No asset</Mui.MenuItem>
        <Mui.MenuItem value={1}>III pillar, pre-2021</Mui.MenuItem>
      </Mui.Select>
      <Mui.Select
        value={data.categories.map((c) => c.id)}
        multiple
        displayEmpty
        className="my-2"
        // onChange={(event) => setSelectedCategories(event.target.value as number[])}
        renderValue={(selected) => (selected.length === 0 ? 'No categories' : `${selected.length} categories`)}
      >
        <Mui.MenuItem value={1}>Food</Mui.MenuItem>
        <Mui.MenuItem value={2}>Rent</Mui.MenuItem>
        <Mui.MenuItem value={3}>Lifestyle</Mui.MenuItem>
      </Mui.Select>
    </>
  );
}
