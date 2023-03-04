import * as React from 'react';
import * as Router from 'react-router-dom';

import Authenticated from './components/authentication/Authenticated';
import Layout from './components/layouts/Layout';
import Sidebar from './components/layouts/Sidebar';
import AssetsIndex from './pages/assets/AssetsIndex';
import HomeIndex from './pages/home/HomeIndex';
import LoginIndex from './pages/login/LoginIndex';
import SettingsIndex from './pages/settings/SettingsIndex';
import TaxesIndex from './pages/taxes/TaxesIndex';
import TransactionsIndex from './pages/transactions/TransactionsIndex';

const withLayout = (element: React.ReactNode, activeTabLabel: string) => (
  <Layout sidebar={<Sidebar activeTabLabel={activeTabLabel} />}>{element}</Layout>
);

const router = Router.createBrowserRouter(
  Router.createRoutesFromElements(
    <>
      <Router.Route path="/login" element={<LoginIndex />} />
      <Router.Route path="/" element={<Authenticated>{withLayout(<HomeIndex />, 'home')}</Authenticated>} />
      <Router.Route path="/transactions" element={<Authenticated>{withLayout(<TransactionsIndex />, 'transactions')}</Authenticated>} />
      <Router.Route path="/assets" element={<Authenticated>{withLayout(<AssetsIndex />, 'assets')}</Authenticated>} />
      <Router.Route path="/taxes" element={<Authenticated>{withLayout(<TaxesIndex />, 'taxes')}</Authenticated>} />
      <Router.Route path="/settings" element={<Authenticated>{withLayout(<SettingsIndex />, 'settings')}</Authenticated>} />
    </>
  )
);

export default router;
