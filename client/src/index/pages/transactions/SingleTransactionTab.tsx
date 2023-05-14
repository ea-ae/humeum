import * as Mui from '@mui/material';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import dayjs from 'dayjs';

import { AssetDto, BriefRelatedResourceDto, TaxSchemeDto, TransactionDto } from '../../api/api';
import CurrencyInput from '../../components/cards/CurrencyInput';
import Input from '../../components/cards/Input';

// lazy so doing transaction types this way

interface TransactionType {
  id: number;
  name: string;
  code: string;
}

const TRANSACTION_TYPES: TransactionType[] = [
  { id: 1, name: 'Always', code: 'ALWAYS' },
  { id: 2, name: 'Pre-retirement only', code: 'PRERETIREMENTONLY' },
  { id: 3, name: 'Retirement only', code: 'RETIREMENTONLY' },
];

interface Props {
  data: TransactionDto;
  setData: (data: TransactionDto) => void;
  taxSchemes: TaxSchemeDto[];
  assets: AssetDto[];
}

export default function SingleTransactionTab({ data, setData, taxSchemes, assets }: Props) {
  const setTaxScheme = (event: Mui.SelectChangeEvent<number>) => {
    const taxSchemeId = event.target.value as number;
    const taxScheme = taxSchemes.find((t) => t.id === taxSchemeId);
    if (taxScheme === undefined) throw new Error(`Tax scheme with id ${event.target.value} not found`);

    setData(
      new TransactionDto({
        ...data,
        taxScheme: new BriefRelatedResourceDto({
          id: event.target.value as number,
          name: taxScheme.name,
        }),
      })
    );
  };

  const setAsset = (event: Mui.SelectChangeEvent<number>) => {
    const assetId = event.target.value as number;

    if (assetId === -1) {
      setData(
        new TransactionDto({
          ...data,
          asset: undefined,
        })
      );
      return;
    }

    const asset = assets.find((a) => a.id === assetId);
    if (asset === undefined) throw new Error(`Asset with id ${event.target.value} not found`);

    setData(
      new TransactionDto({
        ...data,
        asset: new BriefRelatedResourceDto({
          id: event.target.value as number,
          name: asset.name,
        }),
      })
    );
  };

  const setTransactionType = (event: Mui.SelectChangeEvent<number>) => {
    const transactionType = TRANSACTION_TYPES.find((tt) => tt.id === (event.target.value as number)) as TransactionType;

    setData(
      new TransactionDto({
        ...data,
        typeCode: transactionType.code,
        typeName: transactionType.name,
      })
    );
  };

  return (
    <>
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
      <Mui.Select
        value={(TRANSACTION_TYPES.find((tt) => tt.code === data.typeCode) as TransactionType).id}
        className="my-2"
        onChange={setTransactionType}
      >
        {TRANSACTION_TYPES.map((transactionType) => (
          <Mui.MenuItem value={transactionType.id} key={transactionType.id}>
            {transactionType.name}
          </Mui.MenuItem>
        ))}
      </Mui.Select>
      <Input
        label="Description"
        defaultValue={data.description ?? ''}
        typePattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,400}$/}
        validPattern={/^[A-Za-z0-9ÕÄÖÜõäöü,.;:!? ]{0,400}$/}
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
      <Mui.Select value={data.taxScheme.id} className="my-2" onChange={setTaxScheme}>
        {taxSchemes.map((taxScheme) => (
          <Mui.MenuItem value={taxScheme.id} key={taxScheme.id}>
            {taxScheme.name}
          </Mui.MenuItem>
        ))}
      </Mui.Select>
      {/* onChange={(event) => setSelectedAsset(event.target.value as number) */}
      <Mui.Select value={data.asset?.id ?? -1} className="my-2" onChange={setAsset}>
        <Mui.MenuItem value={-1}>No asset</Mui.MenuItem>
        {assets.map((asset) => (
          <Mui.MenuItem value={asset.id} key={asset.id}>
            {asset.name}
          </Mui.MenuItem>
        ))}
        {/* <Mui.MenuItem value={-1}>No asset</Mui.MenuItem>
        <Mui.MenuItem value={1}>III pillar, pre-2021</Mui.MenuItem> */}
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
