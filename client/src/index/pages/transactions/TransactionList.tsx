import * as Mui from '@mui/material';
import { DataGrid, GridColDef, GridFooter, GridFooterContainer } from '@mui/x-data-grid';
import * as React from 'react';

function CustomFooter() {
  return (
    <GridFooterContainer>
      <Mui.Button variant="outlined" className="mx-4 whitespace-nowrap">
        Create new
      </Mui.Button>
      <GridFooter />
    </GridFooterContainer>
  );
}

const footer = () => <CustomFooter />; // defined outside of render

function TransactionList() {
  const [pageSize, setPageSize] = React.useState<number>(10);

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
      field: 'category',
      headerName: 'Category',
      type: 'singleSelect',
      flex: 1,
      minWidth: 100,
      editable: true,
      valueOptions: ['Essentials', 'Luxuries', 'Salary', 'Investing'],
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

  const transactions = [
    { id: 18, name: 'Groceries', amount: -50, date: '2023-03-06', category: 'Essentials' },
    { id: 17, name: 'S&P500 index fund', amount: -500, date: '2023-03-05', category: 'Investing' },
    { id: 16, name: 'III pillar fund', amount: -330, date: '2023-03-05', category: 'Investing' },
    { id: 15, name: 'Programming job salary', amount: 2200, date: '2023-03-05', category: 'Salary' },
    { id: 14, name: 'Entertainment', amount: -200, date: '2023-03-01', category: 'Luxuries' },
    { id: 13, name: 'Rent', amount: -650, date: '2023-02-30', category: 'Essentials' },
    { id: 12, name: 'Groceries', amount: -50, date: '2023-02-27', category: 'Essentials' },
    { id: 11, name: 'Freelancing gig', amount: 500, date: '2023-02-25', category: 'Salary' },
    { id: 10, name: 'Car repair', amount: -400, date: '2023-02-22', category: 'Essentials' },
    { id: 9, name: 'Groceries', amount: -50, date: '2023-02-20', category: 'Essentials' },
    { id: 8, name: 'Groceries', amount: -50, date: '2023-02-13', category: 'Essentials' },
    { id: 7, name: 'Groceries', amount: -50, date: '2023-02-06', category: 'Essentials' },
    { id: 6, name: 'S&P500 index fund', amount: -500, date: '2023-02-05', category: 'Investing' },
    { id: 5, name: 'III pillar fund', amount: -330, date: '2023-02-05', category: 'Investing' },
    { id: 4, name: 'Programming job salary', amount: 2200, date: '2023-02-05', category: 'Salary' },
    { id: 3, name: 'Entertainment', amount: -200, date: '2023-02-01', category: 'Luxuries' },
    { id: 2, name: 'Groceries', amount: -50, date: '2023-01-30', category: 'Essentials' },
    { id: 1, name: 'Rent', amount: -650, date: '2023-01-30', category: 'Essentials' },
  ];

  return (
    <DataGrid
      rows={transactions}
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
        Footer: footer,
      }}
    />
  );
}

export default TransactionList;
