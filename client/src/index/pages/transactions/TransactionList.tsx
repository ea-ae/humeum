import * as Mui from '@mui/material';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import axios from 'axios';
import * as React from 'react';

import { TransactionDto, TransactionsClient } from '../../api/api';
import useAuth from '../../hooks/useAuth';
import useCache from '../../hooks/useCache';
import TransactionListFooter from './TransactionListFooter';

function TransactionList() {
  const [pageSize, setPageSize] = React.useState<number>(10);
  const [transactions, setTransactions] = useCache<TransactionDto[] | null>('transactions', null);
  const { user } = useAuth();

  React.useEffect(() => {
    const cancelSource = axios.CancelToken.source();

    if (user !== null && transactions === null) {
      const client = new TransactionsClient();
      client
        .getTransactions(user.profiles[0].id, user.id.toString(), undefined, undefined, undefined, undefined, cancelSource.token)
        .then((res) => {
          setTransactions(res.result);
        });
    }

    return () => cancelSource.cancel();
  }, []);

  const transactionRows = React.useMemo(() => {
    if (transactions !== null) {
      const rows = transactions.map((transaction) => {
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
      return rows;
    }
    return [];
  }, [transactions]);

  const editTransaction = (id: number) => {
    // eslint-disable-next-line no-console
    console.log(`Editing transaction with id ${id}`);
  };

  const columns: GridColDef[] = [
    { field: 'name', headerName: 'Name', flex: 2, minWidth: 180, editable: true },
    {
      field: 'amount',
      headerName: 'Amount',
      type: 'number',
      flex: 1,
      minWidth: 100,
      editable: true,
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
      editable: true,
      valueOptions: ['Income tax', 'III pillar post-2021', 'III pillar pre-2021', 'Non-taxable'],
    },
    {
      field: 'asset',
      headerName: 'Asset',
      type: 'singleSelect',
      flex: 1,
      minWidth: 100,
      editable: true,
      valueOptions: ['Index fund', 'Bond fund'],
    },
    {
      field: 'editAction',
      headerName: 'Edit',
      renderCell: (params) => (
        <Mui.Button
          variant="text"
          className="text-tertiary-300"
          onClick={(e) => {
            e.stopPropagation();
            editTransaction(params.row.id as number);
          }}
        >
          Edit
        </Mui.Button>
      ),
    },
  ];

  // const transactions = [
  //   { id: 18, name: 'Groceries', amount: -50, date: '2023-03-06', category: 'Essentials' },
  //   { id: 17, name: 'S&P500 index fund', amount: -500, date: '2023-03-05', category: 'Investing' },
  //   { id: 16, name: 'III pillar fund', amount: -330, date: '2023-03-05', category: 'Investing' },
  //   { id: 15, name: 'Programming job salary', amount: 2200, date: '2023-03-05', category: 'Salary' },
  //   { id: 14, name: 'Entertainment', amount: -200, date: '2023-03-01', category: 'Luxuries' },
  //   { id: 13, name: 'Rent', amount: -650, date: '2023-02-30', category: 'Essentials' },
  //   { id: 12, name: 'Groceries', amount: -50, date: '2023-02-27', category: 'Essentials' },
  //   { id: 11, name: 'Freelancing gig', amount: 500, date: '2023-02-25', category: 'Salary' },
  //   { id: 10, name: 'Car repair', amount: -400, date: '2023-02-22', category: 'Essentials' },
  //   { id: 9, name: 'Groceries', amount: -50, date: '2023-02-20', category: 'Essentials' },
  //   { id: 8, name: 'Groceries', amount: -50, date: '2023-02-13', category: 'Essentials' },
  //   { id: 7, name: 'Groceries', amount: -50, date: '2023-02-06', category: 'Essentials' },
  //   { id: 6, name: 'S&P500 index fund', amount: -500, date: '2023-02-05', category: 'Investing' },
  //   { id: 5, name: 'III pillar fund', amount: -330, date: '2023-02-05', category: 'Investing' },
  //   { id: 4, name: 'Programming job salary', amount: 2200, date: '2023-02-05', category: 'Salary' },
  //   { id: 3, name: 'Entertainment', amount: -200, date: '2023-02-01', category: 'Luxuries' },
  //   { id: 2, name: 'Groceries', amount: -50, date: '2023-01-30', category: 'Essentials' },
  //   { id: 1, name: 'Rent', amount: -650, date: '2023-01-30', category: 'Essentials' },
  // ];

  return (
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
  );
}

export default TransactionList;
