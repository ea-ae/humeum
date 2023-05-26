import { DataGrid, GridColDef } from '@mui/x-data-grid';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import { BriefRelatedResourceDto, TransactionDto, TransactionsClient } from '../../api/api';
import fetchTransactions from '../../api/fetchTransactions';
import useAuth from '../../hooks/useAuth';
import useCache, { CacheKey } from '../../hooks/useCache';
import EditDialog from './EditDialog';
import TransactionListFooter from './TransactionListFooter';

const createDefaultTransaction = () =>
  new TransactionDto({
    id: -1,
    name: '',
    description: '',
    amount: 1,
    typeName: 'Always',
    typeCode: 'ALWAYS',
    paymentTimelinePeriodStart: new Date(),
    taxScheme: new BriefRelatedResourceDto({ id: 1, name: '' }),
    asset: undefined,
    categories: [],
  });

const gridColumns: GridColDef[] = [
  { field: 'name', headerName: 'Name', flex: 2, minWidth: 180, cellClassName: 'group-hover:text-blue-600', editable: false },
  {
    field: 'amount',
    headerName: 'Amount',
    type: 'number',
    flex: 1,
    minWidth: 50,
    editable: false,
    valueFormatter: (params) => `${params.value} €`,
    cellClassName: (params) => (params.value > 0 ? 'text-green-700' : 'text-red-700'),
  },
  { field: 'date', headerName: 'Date', type: 'date', flex: 1, minWidth: 100, editable: true },
  {
    field: 'taxScheme',
    headerName: 'Tax Scheme',
    type: 'singleSelect',
    flex: 1,
    minWidth: 100,
    editable: false,
    valueOptions: ['Income tax', 'III pillar post-2021', 'III pillar pre-2021', 'Non-taxable'],
  },
  {
    field: 'asset',
    headerName: 'Asset',
    type: 'singleSelect',
    flex: 1,
    minWidth: 100,
    editable: false,
    valueOptions: ['Index fund', 'Bond fund'],
  },
];

function TransactionList() {
  const [transactions, setTransactions] = fetchTransactions(...useCache<TransactionDto[] | null>(CacheKey.Transactions, null));

  const [pageSize, setPageSize] = React.useState<number>(10);
  const [isEditDialogOpen, setIsEditDialogOpen] = React.useState<boolean>(false);
  const [selectedTransaction, setSelectedTransaction] = React.useState<TransactionDto | null>(null);

  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  const fail = () => {
    setAuthentication(null);
    navigate('/login');
  };

  const onDialogClose = () => setIsEditDialogOpen(false); // we do not null the transaction here, because we want to keep it for the transition

  const createTransaction = (transaction: TransactionDto) => {
    if (user !== null && transactions !== null) {
      const client = new TransactionsClient();
      const profileId = user.profiles[0].id;

      // i just don't care at all i need to get this homework done

      const get = async () => {
        const result = await client.addTransaction(
          user.id,
          profileId,
          '1',
          transaction.name,
          transaction.description,
          transaction.amount,
          transaction.typeCode,
          transaction.paymentTimelinePeriodStart,
          transaction.taxScheme.id,
          transaction.asset?.id,
          transaction.paymentTimelinePeriodEnd,
          transaction.paymentTimelineFrequencyTimeUnitCode,
          transaction.paymentTimelineFrequencyTimesPerCycle,
          transaction.paymentTimelineFrequencyUnitsInCycle
        );

        const createdId = parseInt(result.headers.location.split('/').pop() as string, 10);

        transaction.categories.forEach((c) => client.addCategoryToTransaction(profileId, createdId, '1', user.id.toString(), c.id));
        return result;
      };

      const set = () => setTransactions(transactions.concat(transaction));

      // yep yep i know this is terrible ..~((~.. but let it be ..~))~~..

      setSelectedTransaction(null);
      TransactionsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
    }
  };

  const replaceTransaction = (transaction: TransactionDto) => {
    if (user !== null && transactions !== null) {
      const client = new TransactionsClient();
      const profileId = user.profiles[0].id;

      const get = async () => {
        const result = await client.replaceTransaction(
          profileId,
          transaction.id,
          '1',
          user.id.toString(),
          transaction.name,
          transaction.description,
          transaction.amount,
          transaction.typeCode,
          transaction.paymentTimelinePeriodStart,
          transaction.taxScheme.id,
          transaction.asset?.id,
          transaction.paymentTimelinePeriodEnd,
          transaction.paymentTimelineFrequencyTimeUnitCode,
          transaction.paymentTimelineFrequencyTimesPerCycle,
          transaction.paymentTimelineFrequencyUnitsInCycle
        );

        // get a diff of categories to add and remove from the transaction and apply it through API calls
        const originalTransaction = transactions.find((t) => t.id === transaction.id);
        if (originalTransaction === undefined) throw new Error(`Transaction with id ${transaction.id} not found`);

        const originalCategories = originalTransaction.categories.map((c) => c.id);
        const newCategories = transaction.categories.map((c) => c.id);

        const categoriesToAdd = newCategories.filter((c) => !originalCategories.includes(c));
        const categoriesToRemove = originalCategories.filter((c) => !newCategories.includes(c));

        categoriesToAdd.forEach((c) => client.addCategoryToTransaction(profileId, transaction.id, '1', user.id.toString(), c));
        categoriesToRemove.forEach((c) => client.removeCategoryFromTransaction(profileId, transaction.id, '1', user.id.toString(), c));

        return result;
      };

      const set = () => setTransactions(transactions.map((t) => (t.id === transaction.id ? transaction : t)));

      TransactionsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
    }
  };

  const onTransactionSave = (transaction: TransactionDto) => {
    if (transaction.id === -1) {
      createTransaction(transaction);
    } else {
      replaceTransaction(transaction);
    }
  };

  const transactionRows = React.useMemo(() => {
    if (transactions !== null) {
      return transactions.map((transaction) => {
        const start = transaction.paymentTimelinePeriodStart.toLocaleDateString();
        const end = transaction.paymentTimelinePeriodEnd?.toLocaleDateString();
        const date = end ? `${start} — ${end}` : start;

        return {
          id: transaction.id,
          name: transaction.name ?? 'Unnamed transaction',
          amount: transaction.amount,
          date,
          taxScheme: transaction.taxScheme.name,
          asset: transaction.asset?.name ?? '',
        };
      });
    }
    return [];
  }, [transactions]);

  const onTransactionCreateClick = () => {
    setSelectedTransaction(createDefaultTransaction());
    setIsEditDialogOpen(true);
  };

  const onTransactionEditClick = (id: number) => {
    if (transactions === null) {
      throw new Error('Transactions list not set.');
    }

    const transaction = transactions?.find((t) => t.id === id);
    if (transaction === undefined) {
      throw new Error('Transaction not found.');
    }

    setSelectedTransaction(transaction);
    setIsEditDialogOpen(true);
  };

  const onTransactionDelete = (id: number) => {
    if (user !== null && transactions !== null) {
      const client = new TransactionsClient();
      const profileId = user.profiles[0].id;
      const get = () => client.deleteTransaction(profileId, id, '1', user.id.toString());
      const set = () => setTransactions(transactions.filter((t) => t.id !== id));

      TransactionsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
    }
  };

  const footerConstructor = () => <TransactionListFooter onCreateClick={onTransactionCreateClick} />;

  return (
    <>
      <DataGrid
        rows={transactionRows}
        columns={gridColumns}
        pageSize={pageSize}
        rowsPerPageOptions={[5, 10, 25, 50]}
        onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
        checkboxSelection
        disableSelectionOnClick
        experimentalFeatures={{ newEditingApi: true }}
        classes={{ cell: 'outline-none', columnHeader: 'outline-none', row: 'group' }}
        className="min-w-fit card mr-4"
        components={{
          Footer: footerConstructor,
        }}
        onRowClick={(params) => onTransactionEditClick(params.id as number)}
      />
      {selectedTransaction !== null ? (
        <EditDialog
          transaction={selectedTransaction}
          isOpen={isEditDialogOpen}
          onClose={onDialogClose}
          onSave={onTransactionSave}
          onDelete={onTransactionDelete}
        />
      ) : null}
    </>
  );
}

export default TransactionList;
