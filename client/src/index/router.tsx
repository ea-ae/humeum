import * as React from 'react';
import * as Router from 'react-router-dom';

import Authenticated from './components/authentication/Authenticated';
import Layout from './components/layouts/Layout';
import Sidebar from './components/layouts/Sidebar';
import AssetsIndex from './pages/assets/AssetsIndex';
import HomeIndex from './pages/home/HomeIndex';
import SettingsIndex from './pages/settings/SettingsIndex';
import TaxesIndex from './pages/taxes/TaxesIndex';
import TransactionsIndex from './pages/transactions/TransactionsIndex';

const withLayout = (element: React.ReactNode, activeTabLabel: string) => (
  <Layout sidebar={<Sidebar activeTabLabel={activeTabLabel} />}>{element}</Layout>
);

const router = Router.createBrowserRouter(
  Router.createRoutesFromElements(
    <>
      <Router.Route path="/login" element={<h1>Login</h1>} />
      <Router.Route path="/admin" element={<Router.Navigate to="/login" replace />} />
      <Router.Route
        path="/secret"
        element={
          <Authenticated>
            <b>Secret!</b>
          </Authenticated>
        }
      />

      <Router.Route path="/" element={withLayout(<HomeIndex />, 'home')} />
      <Router.Route path="/transactions" element={withLayout(<TransactionsIndex />, 'transactions')} />
      <Router.Route path="/assets" element={withLayout(<AssetsIndex />, 'assets')} />
      <Router.Route path="/taxes" element={withLayout(<TaxesIndex />, 'taxes')} />
      <Router.Route path="/settings" element={withLayout(<SettingsIndex />, 'settings')} />
    </>
  )
);

export default router;
