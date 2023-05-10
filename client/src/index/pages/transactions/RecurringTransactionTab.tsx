import * as Mui from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import dayjs from 'dayjs';

import { TransactionDto } from '../../api/api';
import CurrencyInput from '../../components/cards/CurrencyInput';
import Input from '../../components/cards/Input';
import PositiveIntegerInput from '../../components/cards/PositiveIntegerInput';

interface Props {
  data: TransactionDto;
  setData: (data: TransactionDto) => void;
}

export default function RecurringTransactionTab({ data, setData }: Props) {
  return (
    <>
      <PositiveIntegerInput
        label="n times"
        defaultValue={data.paymentTimelineFrequencyTimesPerCycle ?? null}
        isOutlined
        onChange={(n) => setData(new TransactionDto({ ...data, paymentTimelineFrequencyTimesPerCycle: n ?? undefined }))}
      />
      <PositiveIntegerInput
        label="every n"
        defaultValue={data.paymentTimelineFrequencyUnitsInCycle ?? null}
        isOutlined
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
      <Input
        label="Name"
        defaultValue={data.name}
        typePattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,50}$/}
        validPattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{1,50}$/}
        isOutlined
        onChange={(value: string) => setData(new TransactionDto({ ...data, name: value }))}
      />
      <CurrencyInput
        label="Amount"
        defaultValue={data.amount}
        isOutlined
        onChange={(value: number) => setData(new TransactionDto({ ...data, amount: value }))}
      />
      <DatePicker
        label="Start date"
        defaultValue={dayjs(data.paymentTimelinePeriodStart)}
        className="md:col-start-3 my-2"
        onChange={(value) => setData(new TransactionDto({ ...data, paymentTimelinePeriodStart: (value as dayjs.Dayjs).toDate() }))}
      />
      <DatePicker
        label="End date"
        defaultValue={data.paymentTimelinePeriodEnd === undefined ? null : dayjs(data.paymentTimelinePeriodEnd)}
        className="md:col-start-3 my-2"
        onChange={(value) => setData(new TransactionDto({ ...data, paymentTimelinePeriodEnd: (value as dayjs.Dayjs).toDate() }))}
      />
    </>
  );
}
