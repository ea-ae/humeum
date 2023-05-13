import * as React from 'react';
import * as Router from 'react-router-dom';

import Authenticated from './components/authentication/Authenticated';
import Layout from './components/layouts/Layout';
import Sidebar from './components/layouts/Sidebar';
import AssetsPage from './pages/assets/AssetsPage';
import HomePage from './pages/home/HomePage';
import LoginPage from './pages/login/LoginPage';
import RegisterPage from './pages/register/RegisterPage';
import SettingsPage from './pages/settings/SettingsPage';
import TaxesPage from './pages/taxes/TaxesPage';
import TransactionsIndex from './pages/transactions/TransactionsPage';

const withLayout = (element: React.ReactNode, activeTabLabel: string) => (
  <Layout sidebar={<Sidebar activeTabLabel={activeTabLabel} />}>{element}</Layout>
);

const router = Router.createBrowserRouter(
  Router.createRoutesFromElements(
    <>
      <Router.Route path="/register" element={<RegisterPage />} />
      <Router.Route path="/login" element={<LoginPage />} />
      <Router.Route path="/" element={<Authenticated>{withLayout(<HomePage />, 'home')}</Authenticated>} />
      <Router.Route path="/transactions" element={<Authenticated>{withLayout(<TransactionsIndex />, 'transactions')}</Authenticated>} />
      <Router.Route path="/assets" element={<Authenticated>{withLayout(<AssetsPage />, 'assets')}</Authenticated>} />
      <Router.Route path="/taxes" element={<Authenticated>{withLayout(<TaxesPage />, 'taxes')}</Authenticated>} />
      <Router.Route path="/settings" element={<Authenticated>{withLayout(<SettingsPage />, 'settings')}</Authenticated>} />
    </>
  )
);

export default router;
