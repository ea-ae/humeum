import './index.css';

import * as Mui from '@mui/material';
import * as React from 'react';
import ReactDOM from 'react-dom/client';
import * as Router from 'react-router-dom';

import Login from './authentication/Login';
import { ProvideAuth } from './authentication/ProvideAuth';
import AssetsIndex from './pages/assets/AssetsIndex';
import HomeIndex from './pages/home/HomeIndex';
import SettingsIndex from './pages/settings/SettingsIndex';
import TaxesIndex from './pages/taxes/TaxesIndex';
import TransactionsIndex from './pages/transactions/TransactionsIndex';
import Layout from './shared/layouts/Layout';
import Sidebar from './shared/layouts/Sidebar';

const theme = Mui.createTheme({
  components: {
    MuiButtonBase: {
      defaultProps: {
        disableRipple: true,
      },
    },
  },
});

const withLayout = (element: React.ReactNode, activeTabLabel: string) => (
  <Layout sidebar={<Sidebar activeTabLabel={activeTabLabel} />}>{element}</Layout>
);

const router = Router.createBrowserRouter(
  Router.createRoutesFromElements(
    <>
      <Router.Route path="/" element={withLayout(<HomeIndex />, 'home')} />
      <Router.Route path="/login" element={<Login />} />
      <Router.Route path="/admin" element={<Router.Navigate to="/login" replace />} />
      <Router.Route path="/transactions" element={withLayout(<TransactionsIndex />, 'transactions')} />
      <Router.Route path="/assets" element={withLayout(<AssetsIndex />, 'assets')} />
      <Router.Route path="/taxes" element={withLayout(<TaxesIndex />, 'taxes')} />
      <Router.Route path="/settings" element={withLayout(<SettingsIndex />, 'settings')} />
    </>
  )
);

const container = document.getElementById('app');
// eslint-disable-next-line @typescript-eslint/no-non-null-assertion
const root = ReactDOM.createRoot(container!);

root.render(
  <React.StrictMode>
    <Mui.ThemeProvider theme={theme}>
      <ProvideAuth>
        <Router.RouterProvider router={router} />
      </ProvideAuth>
    </Mui.ThemeProvider>
  </React.StrictMode>
);
