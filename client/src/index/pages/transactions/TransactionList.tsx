import * as Mui from '@mui/material';
import { DataGrid, GridColDef, GridRenderCellParams } from '@mui/x-data-grid';
import axios from 'axios';
import * as React from 'react';
import { useNavigate } from 'react-router-dom';

import { TransactionDto, TransactionsClient } from '../../api/api';
import useAuth from '../../hooks/useAuth';
import useCache from '../../hooks/useCache';
import EditDialog from './EditDialog';
import TransactionListFooter from './TransactionListFooter';

const getGridColumns = (onEdit: (rowId: number) => void): GridColDef[] => {
  const onEditClick = (e: React.MouseEvent, params: GridRenderCellParams) => {
    e.preventDefault();
    onEdit(params.row.id as number);
  };

  return [
    { field: 'name', headerName: 'Name', flex: 2, minWidth: 180, editable: false },
    {
      field: 'amount',
      headerName: 'Amount',
      type: 'number',
      minWidth: 100,
      editable: false,
      valueFormatter: (params) => `${params.value} €`,
      cellClassName: (params) => (params.value > 0 ? 'text-green-700' : 'text-red-700'),
    },
    { field: 'date', headerName: 'Date', type: 'date', flex: 1, minWidth: 100, editable: true },
    {
      field: 'taxScheme',
      headerName: 'Tax Scheme',
      type: 'singleSelect',
      minWidth: 100,
      editable: false,
      valueOptions: ['Income tax', 'III pillar post-2021', 'III pillar pre-2021', 'Non-taxable'],
    },
    {
      field: 'asset',
      headerName: 'Asset',
      type: 'singleSelect',
      minWidth: 100,
      editable: false,
      valueOptions: ['Index fund', 'Bond fund'],
    },
    {
      field: 'editAction',
      headerName: 'Edit',
      renderCell: (params) => (
        <Mui.Button variant="text" className="text-tertiary-300" onClick={(e) => onEditClick(e, params)}>
          Edit
        </Mui.Button>
      ),
    },
  ];
};

function TransactionList() {
  const [pageSize, setPageSize] = React.useState<number>(10);
  const [transactions, setTransactions] = useCache<TransactionDto[] | null>('transactions', null);
  const [isEditDialogOpen, setIsEditDialogOpen] = React.useState<boolean>(false);
  const [selectedTransaction, setSelectedTransaction] = React.useState<TransactionDto | null>(null);
  const { user, setAuthentication } = useAuth();
  const navigate = useNavigate();

  const client = new TransactionsClient();

  const fail = () => {
    setAuthentication(null);
    navigate('/login');
  };

  React.useEffect(() => {
    if (user === null) {
      throw new Error('User was null in startup effect');
    }

    const cancelSource = axios.CancelToken.source();

    const userId = user.id.toString();
    const profileId = user.profiles[0].id;

    const get = () => client.getTransactions(profileId, '1', userId, undefined, undefined, undefined, undefined, cancelSource.token);
    const set = (value: TransactionDto[]) => setTransactions(value);
    TransactionsClient.callAuthenticatedEndpoint(get, set, fail, user.id, cancelSource.token);

    return () => cancelSource.cancel();
  }, []);

  const onTransactionSave = (transaction: TransactionDto) => {
    setIsEditDialogOpen(false); // we do not null the transaction here, because we want to keep it for the transition

    if (user !== null && transactions !== null) {
      const userId = user.id.toString();
      const profileId = user.profiles[0].id;

      const get = () =>
        client.replaceTransaction(
          profileId,
          transaction.id,
          '1',
          userId,
          transaction.name,
          transaction.description,
          transaction.amount,
          transaction.typeCode,
          transaction.paymentTimelinePeriodStart,
          transaction.taxScheme.id,
          transaction.asset?.id
        );
      const set = () => setTransactions(transactions.map((t) => (t.id === transaction.id ? transaction : t)));
      TransactionsClient.callAuthenticatedEndpoint(get, set, fail, user.id);
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

  const onTransactionEditClick = (id: number) => {
    if (transactions === null) {
      throw new Error('Transactions list not set.');
    }

    const transaction = transactions?.find((t) => t.id === id);
    if (transaction === undefined) {
      throw new Error('Transaction not found.');
    }

    setIsEditDialogOpen(true);
    setSelectedTransaction(transaction);
  };

  const columns: GridColDef[] = getGridColumns(onTransactionEditClick);

  return (
    <>
      <DataGrid
        rows={transactionRows}
        columns={columns}
        pageSize={pageSize}
        rowsPerPageOptions={[5, 10, 25, 50]}
        onPageSizeChange={(newPageSize) => setPageSize(newPageSize)}
        checkboxSelection
        disableSelectionOnClick
        experimentalFeatures={{ newEditingApi: true }}
        classes={{ cell: 'outline-none', columnHeader: 'outline-none' }}
        className="min-w-fit card"
        components={{
          Footer: TransactionListFooter,
        }}
      />
      <EditDialog transaction={selectedTransaction} isOpen={isEditDialogOpen} onSave={onTransactionSave} />
    </>
  );
}

export default TransactionList;
