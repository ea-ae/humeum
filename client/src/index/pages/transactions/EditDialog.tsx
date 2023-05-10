import * as Mui from '@mui/material';
import { useTheme } from '@mui/material/styles';
import useMediaQuery from '@mui/material/useMediaQuery';
import * as React from 'react';

import { AssetDto, TaxSchemeDto, TransactionDto } from '../../api/api';
import fetchTaxSchemes from '../../api/fetchTaxSchemes';
import useCache, { CacheKey } from '../../hooks/useCache';
import RecurringTransactionTab from './RecurringTransactionTab';
import SingleTransactionTab from './SingleTransactionTab';

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

  onClose: () => void;
  onSave: (transaction: TransactionDto) => void;
}

export default function EditDialog({ transaction, isOpen, onClose, onSave }: Props) {
  const [taxSchemes, _setTaxSchemes] = fetchTaxSchemes(...useCache<TaxSchemeDto[] | null>(CacheKey.TaxSchemes, null));

  const [assets, _setAssets] = useCache<AssetDto[] | null>(CacheKey.Assets, null);

  const [data, setData] = React.useState<TransactionDto>(transaction);
  // const [selectedAsset, setSelectedAsset] = React.useState<number>(transaction.asset?.id ?? -1);
  // const [selectedCategories, setSelectedCategories] = React.useState<number[]>(transaction.categories.map((c) => c.id));

  const [activeTab, setActiveTab] = React.useState<number>(EditDialogTab.SINGLE_TRANSACTION);

  React.useEffect(() => {
    setData(transaction);
    // setSelectedAsset(transaction.asset?.id ?? -1);
    // setSelectedCategories(transaction.categories.map((c) => c.id));
    setActiveTab(EditDialogTab.SINGLE_TRANSACTION);
  }, [transaction.id]); // whenever the default value changes, reset the state

  const theme = useTheme();
  const fullScreen = useMediaQuery(theme.breakpoints.down('md'));

  if (taxSchemes === null) return null;

  const onTransactionSave = () => {
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
    onClose();
  };

  return (
    <Mui.Dialog
      open={isOpen}
      onClose={onClose}
      fullScreen={fullScreen}
      classes={{ paper: 'md:min-w-[70vw] lg:min-w-[65vw] xl:min-w-[38w] 2xl:min-w-[25w]' }}
    >
      <Mui.DialogContent dividers>
        <Mui.Tabs value={activeTab} centered={!fullScreen} variant="fullWidth" onChange={(_, value) => setActiveTab(value)}>
          <Mui.Tab label="Single" />
          <Mui.Tab label="Recurring" />
        </Mui.Tabs>
        <Mui.Divider />
        <div className="grid grid-cols-1 md:grid-cols-3 gap-x-4 gap-y-1 my-4">
          {activeTab === EditDialogTab.SINGLE_TRANSACTION ? (
            <SingleTransactionTab data={data} setData={setData} taxSchemes={taxSchemes} />
          ) : null}
          {activeTab === EditDialogTab.RECURRING_TRANSACTION ? <RecurringTransactionTab data={data} setData={setData} /> : null}
        </div>
      </Mui.DialogContent>
      <Mui.DialogActions>
        <Mui.Button onClick={onTransactionSave}>
          {activeTab === EditDialogTab.SINGLE_TRANSACTION ? 'Save as a single-time transaction' : null}
          {activeTab === EditDialogTab.RECURRING_TRANSACTION ? 'Save as a recurring transaction' : null}
        </Mui.Button>
      </Mui.DialogActions>
    </Mui.Dialog>
  );
}
