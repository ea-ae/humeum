import * as React from 'react';
import * as ReactRouter from 'react-router-dom';
import ReactDOM from 'react-dom';
import * as Mui from '@mui/material';

import Layout from '../shared/Layout';
import './index.css';
import Welcome from './home/Welcome';
import TransactionList from './transactions/TransactionList';

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
        element: <Welcome username="admin" savedUp={42_350} haveToSave={4650} retireInYears={23} />,
    },
    {
        path: '/transactions',
        element: <TransactionList />,
    },
]);

ReactDOM.render(
    <Mui.ThemeProvider theme={theme}>
        <Layout sidebar={true}>
            <ReactRouter.RouterProvider router={router} />
        </Layout>
    </Mui.ThemeProvider>,
    document.getElementById('app')
);
