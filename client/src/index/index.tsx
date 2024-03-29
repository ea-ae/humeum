import './index.css';

import * as Mui from '@mui/material';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
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

const container = document.getElementById('app') as HTMLElement;
const root = ReactDOM.createRoot(container);

// StrictMode causes a double render
root.render(
  <React.StrictMode>
    <Mui.ThemeProvider theme={theme}>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <ProvideAuth>
          <Router.RouterProvider router={router} />
        </ProvideAuth>
      </LocalizationProvider>
    </Mui.ThemeProvider>
  </React.StrictMode>
);
