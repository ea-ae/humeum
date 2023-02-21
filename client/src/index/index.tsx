import './index.css';

import * as Mui from '@mui/material';
import * as React from 'react';
import ReactDOM from 'react-dom/client';
import * as Router from 'react-router-dom';

import ProvideAuth from './components/authentication/ProvideAuth';
import router from './router';

const theme = Mui.createTheme({
  components: {
    MuiButtonBase: {
      defaultProps: {
        disableRipple: false,
      },
    },
  },
});

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
