using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Goodware.Jabber.Library
{
    /// <summary>
    /// Класа со методи за автентикација
    /// </summary>
    public class Authenticator
    {
        static RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        SHA1Managed sha;
        public Authenticator()
        {
            sha = new SHA1Managed();
            sha.Initialize();
        }

        /// <summary>
        /// Врши конверзија од бајти во хекса стринг. Сите карактери се мали (според стандард)
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string GetAsHexaDecimal(byte[] bytes)
        {
            StringBuilder s = new StringBuilder();
            int length = bytes.Length;
            for (int n = 0; n < length; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }

        /// <summary>
        /// Генерира квази-случаен токен
        /// </summary>
        /// <returns></returns>
        public static String randomToken()
        {
            
            // Додаден static модификатор од Дарко

            byte[] randomNumber = new byte[10];
            random.GetBytes(randomNumber);
            return random.GetHashCode().ToString("X");
        }

        /// <summary>
        /// Враќа sha код за streamID и password
        /// </summary>
        /// <param name="streamID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public String getDigest(String streamID, String password)
        {
            byte[] rez = sha.ComputeHash(Encoding.UTF8.GetBytes(streamID + password));
            return GetAsHexaDecimal(rez);
        }

        /// <summary>
        /// Проверка дали се поклопуваат двата digest-а
        /// </summary>
        /// <param name="streamID"></param>
        /// <param name="password"></param>
        /// <param name="digest"></param>
        /// <returns></returns>
        public Boolean isDigestAuthenticated(String streamID, String password, String digest)
        {
            return digest.Equals(getDigest(streamID, password));
        }

        /// <summary>
        /// Метод за Zero-Knowledge автентикација (проблематичниот дел), сега работи супер
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="token"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public String getZeroKHash(int sequence, byte[] token, byte[] password)
        {
            byte[] runningHash = sha.ComputeHash(password);
            runningHash = Encoding.UTF8.GetBytes(GetAsHexaDecimal(runningHash));
            List<byte> list1 = new List<byte>(runningHash);
            List<byte> list2 = new List<byte>(token);
            list1.AddRange(list2);
            byte[] rez = list1.ToArray();
            runningHash = sha.ComputeHash(rez);
            for (int i = 0; i < sequence; i++)
            {
                runningHash = sha.ComputeHash(Encoding.UTF8.GetBytes(GetAsHexaDecimal(runningHash)));
            }
            return GetAsHexaDecimal(runningHash);
        }

        /// <summary>
        /// Порверка дали корисникот е точна лозинката (дали се поклопуваат двата хаша)
        /// </summary>
        /// <param name="userHash"></param>
        /// <param name="testHash"></param>
        /// <returns></returns>
        public Boolean isHashAuthenticated(String userHash, String testHash)
        {
            testHash = GetAsHexaDecimal(sha.ComputeHash(Encoding.UTF8.GetBytes(testHash)));
            return testHash.Equals(userHash);
        }
    }
}
