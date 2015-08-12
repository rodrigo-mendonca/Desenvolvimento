# Puppet configurations
 
class base {
 
## Update apt-get ##
  exec { 'apt-get update':
    command => '/usr/bin/apt-get update'
  }
}
 
class http{
    package { "apache2":
        ensure => present,
    }
 
    service { "apache2":
        ensure => running,
        require => Package["apache2"],
    }
}
 
include base
include http