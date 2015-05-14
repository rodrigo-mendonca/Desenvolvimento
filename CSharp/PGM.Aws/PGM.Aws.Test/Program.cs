using System;
using Amazon.EC2;
using System.Configuration;
using System.Collections.Specialized;
using Amazon;
using Amazon.EC2.Model;

namespace PGM.Aws.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            AmazonEC2Config config = new AmazonEC2Config();
            config.ServiceURL = "54.207.26.121";
            config.RegionEndpoint = RegionEndpoint.SAEast1;

            NameValueCollection appConfig = ConfigurationSettings.AppSettings;
            IAmazonEC2 ec2 = AWSClientFactory.CreateAmazonEC2Client(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"], config
                    );


            Console.WriteLine("Instancias");
            DescribeInstancesResponse result  = ec2.DescribeInstances();

            foreach (Reservation reserva in result.Reservations)
            {
                foreach (var instance in reserva.Instances)
                    Console.WriteLine(instance.InstanceType.Value);
            }

            Console.WriteLine("Volumes");
            DescribeVolumesResponse listvol = ec2.DescribeVolumes();

            foreach (Volume vol in listvol.Volumes)
            {
                Console.WriteLine(vol.VolumeId + " Tamanho=" + vol.Size.ToString());
            }

            Console.ReadKey();
        }
    }
}
