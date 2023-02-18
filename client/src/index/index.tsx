import './index.css';

import * as Mui from '@mui/material';
import * as React from 'react';
import ReactDOM from 'react-dom';
import * as ReactRouter from 'react-router-dom';

import Layout from '../shared/Layout';
import HomeIndex from './home/HomeIndex';
import Sidebar from './Sidebar';
import TransactionList from './transactions/TransactionList';

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
      <HomeIndex
        username="admin"
        savedUp={42_350}
        haveToSave={4650}
        retireInYears={23}
      />
    ),
  },
  {
    path: '/transactions',
    element: <TransactionList />,
  },
]);

ReactDOM.render(
  <React.StrictMode>
    <Mui.ThemeProvider theme={theme}>
      <Layout sidebar={<Sidebar activeTab={0} />}>
        <ReactRouter.RouterProvider router={router} />
      </Layout>
    </Mui.ThemeProvider>
  </React.StrictMode>,
  document.getElementById('app')
);
