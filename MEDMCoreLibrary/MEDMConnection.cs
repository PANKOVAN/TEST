using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MEDMCoreLibrary
{
    #region ConnectionPool
    public class MEDMDataConnectionPool
    {
        #region OnNewConnection
        public delegate void OnNewConnectionHandler(MEDMConnection connection);
        public event OnNewConnectionHandler OnNewConnection;
        #endregion
        #region ConnectionPool
        private MEDMConnection[] _ConnectionPool = null;
        private MEDMConnection[] ConnectionPool
        {
            get
            {
                if (_ConnectionPool == null)
                {
                    ResetConnectionPool();
                }
                return _ConnectionPool;
            }
        }
        private void ResetConnectionPool()
        {
            lock (this)
            {
                if (_ConnectionPool != null)
                {
                    for (int i = 0; i < ConnectionPool.Length; i++)
                    {
                        if (ConnectionPool[i] != null)
                        {
                            if (ConnectionPool[i].Connection.State != System.Data.ConnectionState.Closed && !ConnectionPool[i].IsBusy)
                            {
                                ConnectionPool[i].Connection.Close();
                            }
                            ConnectionPool[i] = null;
                        }
                    }
                }
                _ConnectionPool = new MEDMConnection[10];
            }
        }
        private MEDMConnection GetNewConnection()
        {
            MEDMConnection connection = new MEDMConnection(ConnectionString);
            if (OnNewConnection != null) OnNewConnection(connection);
            return connection;
        }
        #endregion
        #region Open/Close
        public void Open()
        {
            ResetConnectionPool();
            FreeConnection(GetConnection());
        }
        public void Close()
        {
            ResetConnectionPool();
        }
        #endregion
        #region ConnectionString
        public string ConnectionString { get; set; }
        #endregion
        #region GetConnection
        public SqlConnection GetConnection()
        {
            SqlConnection result = null;
            lock (this)
            {
                for (int i = 0; i < ConnectionPool.Length; i++)
                {
                    if (ConnectionPool[i] == null)
                    {
                        ConnectionPool[i] = GetNewConnection();

                    }
                    if (ConnectionPool[i].IsError)
                    {
                        ConnectionPool[i] = null;
                        throw new Exception($"Ошибка соединений с базой данных.");
                    }
                    if (ConnectionPool[i].IsBusy) continue;
                    ConnectionPool[i].IsBusy = true;
                    try
                    {
                        switch (ConnectionPool[i].Connection.State)
                        {
                            case System.Data.ConnectionState.Broken:
                                ConnectionPool[i].Connection.Close();
                                ConnectionPool[i].Connection.Open();
                                break;
                            case System.Data.ConnectionState.Closed:
                                ConnectionPool[i].Connection.Open();
                                break;
                            case System.Data.ConnectionState.Open:
                                break;
                            default:
                                throw new Exception("Состояние соединения не определено.");
                        }
                        result = ConnectionPool[i].Connection;
                    }
                    catch (Exception e)
                    {
                        ConnectionPool[i].IsBusy = false;
                        ConnectionPool[i].IsError = true;
                        throw new Exception($"Ошибка соединений с базой данных {e}");
                    }
                    break;
                }
            }
            if (result == null) throw new Exception("Пул соединений переполнен.");
            return result;
        }
        #endregion
        #region FreeConnection
        public void FreeConnection(SqlConnection connection)
        {
            lock (this)
            {
                for (int i = 0; i < ConnectionPool.Length; i++)
                {
                    if (ConnectionPool[i] != null && ConnectionPool[i].Connection == connection)
                    {
                        ConnectionPool[i].IsBusy = false;
                        break;
                    }
                }
            }
        }
        #endregion
    }
    #endregion
    public class MEDMConnection
    {
        public bool IsBusy = false;
        public bool IsError = false;
        public SqlConnection Connection = null;

        public MEDMConnection(string connectionstring)
        {
                Connection = new SqlConnection(connectionstring);
                //Connection.Open();
                //Connection.Close();
        }
    }
}
