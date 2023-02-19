import './index.css';

import * as Mui from '@mui/material';
import * as React from 'react';
import ReactDOM from 'react-dom/client';
import * as ReactRouter from 'react-router-dom';

import Layout from '../shared/Layout';
import HomeIndex from './home/HomeIndex';
import Sidebar from './Sidebar';
import TransactionsIndex from './transactions/TransactionsIndex';
import AssetsIndex from './assets/AssetsIndex';
import AddItemDial from './AddItemDial';

const theme = Mui.createTheme({
  components: {
    MuiButtonBase: {
      defaultProps: {
        disableRipple: true,
      },
    },
  },
});

const router = ReactRouter.createBrowserRouter([
  {
    path: '/',
    element: (
      <Layout sidebar={<Sidebar activeTabLabel="home" />}>
        <HomeIndex username="admin" savedUp={42_350} haveToSave={4650} retireInYears={23} />
        <AddItemDial />
      </Layout>
    ),
  },
  {
    path: '/transactions',
    element: (
      <Layout sidebar={<Sidebar activeTabLabel="transactions" />}>
        <TransactionsIndex />
        <AddItemDial />
      </Layout>
    ),
  },
  {
    path: '/assets',
    element: (
      <Layout sidebar={<Sidebar activeTabLabel="assets" />}>
        <AssetsIndex />
        <AddItemDial />
      </Layout>
    )
  }
]);

const container = document.getElementById('app');
// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
const root = ReactDOM.createRoot(container!);

root.render(
  <React.StrictMode>
    <Mui.ThemeProvider theme={theme}>
      <ReactRouter.RouterProvider router={router} />
    </Mui.ThemeProvider>
  </React.StrictMode>
);
