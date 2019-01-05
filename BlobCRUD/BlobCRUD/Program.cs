using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLOB.CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 0;
            Program mainObject = new Program();

            do
            {
                // Menu - driven program for BLOB CRUD opereations

                Console.WriteLine("\n\nMENU:\n\n1. Insert\n2. Update\n3. Delete\n4. Retrieve\n5. Exit");
                Console.WriteLine("\nEnter your choice : ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        mainObject.Insert();
                        break;
                    case 2:
                        mainObject.Update();
                        break;
                    case 3:
                        mainObject.Delete();
                        break;
                    case 4:
                        mainObject.Retrieve();
                        break;
                    case 5:
                        break;
                }

            } while (choice != 5);


        }

        // Blob Insertion
        public void Insert()
        {
            StorageCredentials storageCredentials = new StorageCredentials("mycarstorage", "TlX57leCmr9/5oswVDyq0p1FcHVY+3szY1cFhL86/wjPzbeLpldjm9BKbbFq+3DZh+gC00WajBNDRtajMQN1Ww==");
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("m1049291blob");

            Console.WriteLine("\nName of the file to be created : ");
            string name = Console.ReadLine();

            string localFileName = name + ".txt";

            Console.WriteLine("\nEnter the data to be inserted in the file :\n");
            string data = Console.ReadLine();

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
            cloudBlockBlob.UploadText(data);

            Console.WriteLine("\nSuccessfully Uploaded !!\n");
        }

        // Blob Updation
        public void Update()
        {
            StorageCredentials storageCredentials = new StorageCredentials("mycarstorage", "TlX57leCmr9/5oswVDyq0p1FcHVY+3szY1cFhL86/wjPzbeLpldjm9BKbbFq+3DZh+gC00WajBNDRtajMQN1Ww==");
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("m1049291blob");

            Console.WriteLine("\nName of the file to be updated : ");
            string name = Console.ReadLine();

            string localFileName = name + ".txt";

            CloudBlockBlob cloudBlockBlob = null;

            if (cloudBlobContainer.GetBlockBlobReference(localFileName).Exists())
            {
                cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(localFileName);
                Console.WriteLine("\nEnter the data to be inserted in the file :\n");
                string data = Console.ReadLine();

                cloudBlockBlob.UploadText(data);

                Console.WriteLine("\nSuccessfully Updated !!\n");
            }
            else
            {
                Console.WriteLine("\nSorry, the file doesn't exist !!\n");
            }


        }

        // Blob Deletion
        public void Delete()
        {
            Console.WriteLine("\nName of the file to be deleted : ");
            string name = Console.ReadLine();

            StorageCredentials storageCredentials = new StorageCredentials("mycarstorage", "TlX57leCmr9/5oswVDyq0p1FcHVY+3szY1cFhL86/wjPzbeLpldjm9BKbbFq+3DZh+gC00WajBNDRtajMQN1Ww==");
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("m1049291blob");

            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(name);
            cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(name + ".txt");

            
            if (cloudBlockBlob.DeleteIfExistsAsync().Result)
            {
                Console.WriteLine("\nSuccessfully Deleted !!\n");
            }
            else
            {
                Console.WriteLine("\nSorry this file doesn't exist !!\n");
            }
        }

        // Retrieva of BOLB file and reading its contents
        public void Retrieve()
        {
            Console.WriteLine("\nName of the file to be retrieved : ");
            string name = Console.ReadLine();

            StorageCredentials storageCredentials = new StorageCredentials("mycarstorage", "TlX57leCmr9/5oswVDyq0p1FcHVY+3szY1cFhL86/wjPzbeLpldjm9BKbbFq+3DZh+gC00WajBNDRtajMQN1Ww==");
            CloudStorageAccount cloudStorageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("m1049291blob");

            name = name + ".txt";

            CloudBlockBlob cloudBlockBlob = null;

            if (cloudBlobContainer.GetBlockBlobReference(name).Exists())
            {
                cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(name);

                string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string localFileName = name + ".txt";

                cloudBlockBlob.DownloadToFile(localPath + "\\" + localFileName, FileMode.Create);

                Console.WriteLine("\n The data in the file is : \n");

                string text = File.ReadAllText(localPath + "\\" + localFileName, Encoding.UTF8);

                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("\nFile doesn't exist !!\n");
            }

        }
    }
}
