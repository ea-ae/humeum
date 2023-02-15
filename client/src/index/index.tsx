import React from 'react';
import ReactDOM from 'react-dom';
import * as Mui from '@mui/material';

import NavigationBar from './NavigationBar';
import Main from './Main';
import './index.css';

const theme = Mui.createTheme({
    components: {
        MuiButtonBase: {
            defaultProps: {
                disableRipple: true,
            }
        }
    }
});

ReactDOM.render(
    <Mui.ThemeProvider theme={theme}>
        <div className="h-screen flex flex-col bg-stone-50">
            <NavigationBar />
            <Main />
        </div>
    </Mui.ThemeProvider>,
    document.getElementById('app')
);
