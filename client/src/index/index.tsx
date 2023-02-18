import * as React from 'react';
import * as ReactRouter from 'react-router-dom';
import ReactDOM from 'react-dom';
import * as Mui from '@mui/material';

import Layout from '../shared/Layout';
import './index.css';
import HomeIndex from './home/HomeIndex';

import TransactionList from './transactions/TransactionList';
import Sidebar from './Sidebar';


const theme = Mui.createTheme({
    components: {
        MuiButtonBase: {
            defaultProps: {
                disableRipple: true,
            }
        }
    }
});

const router = ReactRouter.createBrowserRouter([
    {
        path: '/',
        element: <HomeIndex username="admin" savedUp={42_350} haveToSave={4650} retireInYears={23} />,
    },
    {
        path: '/transactions',
        element: <TransactionList />,
    },
]);

ReactDOM.render(
    <React.StrictMode>
        <Mui.ThemeProvider theme={theme}>
            <Layout sidebar={<Sidebar />}>
                <ReactRouter.RouterProvider router={router} />
            </Layout>
        </Mui.ThemeProvider>
    </React.StrictMode>,
    document.getElementById('app')
);
